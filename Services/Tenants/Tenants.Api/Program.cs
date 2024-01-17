using Contracts.TenantsServiceEvents;
using MassTransit;
using Quartz;
using System.Reflection;
using Tenants.Application.Commands.CreateOrUpdateTenant;
using Tenants.Application.Consumers;
using Tenants.Application.Settings;
using Tenants.Application.Workers;
using Tenants.Domain.Interfaces;
using Tenants.Infrastructure.Repositories;
using Tenants.Infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<ITenantsRepository, TenantsRepository>();
builder.Services.AddSingleton<ITenantsStatisticsRepository, TenantsStatisticsRepository>();

builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQ"));


builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(CreateOrUpdateTenantCommandHandler)));

builder.Services.AddQuartz(options =>
{
    var jobKey = JobKey.Create(nameof(RoomCheckBackgroundJob));

    options
    .AddJob<RoomCheckBackgroundJob>(jobKey)
    .AddTrigger(trigger =>
    {
        trigger.ForJob(jobKey).WithSimpleSchedule(schedule => schedule.WithIntervalInMinutes(2).RepeatForever());
    });
});



builder.Services.AddMassTransit(cfg =>
{
    cfg.SetDefaultEndpointNameFormatter();
    cfg.AddConsumer<RoomDeletedConsumer>();


    cfg.UsingRabbitMq((context, configuration) =>
    {

        configuration.Host(builder.Configuration["RabbitMQ:HostName"], "/", h =>
        {

            h.Username(builder.Configuration["RabbitMQ:UserName"]);
            h.Password(builder.Configuration["RabbitMQ:Password"]);
        });

        configuration.ConfigureEndpoints(context);
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen((setup) =>
{
    var commentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var commentFullPath = Path.Combine(AppContext.BaseDirectory, commentFile);

    setup.IncludeXmlComments(commentFullPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();


app.Run();

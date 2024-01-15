using MassTransit;
using MassTransit.DependencyInjection;
using Quartz;
using Rooms.Application.Commands.CreateOrUpdateRoom;
using Rooms.Application.Consumers;
using Rooms.Application.Settings;
using Rooms.Application.Worker;
using Rooms.Domain.Interfaces;
using Rooms.Infrastructure.Repositories;
using Rooms.Infrastructure.Settings;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQ"));


builder.Services.AddSingleton<IRoomsRepository, RoomsRepository>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(CreateOrUpdateRoomCommandHandler)));

builder.Services.AddQuartz(options =>
{
    var jobKey = JobKey.Create(nameof(ApartmentCheckBackgroundJob));

    options
    .AddJob<ApartmentCheckBackgroundJob>(jobKey)
    .AddTrigger(trigger =>
    {
        trigger.ForJob(jobKey).WithSimpleSchedule(schedule => schedule.WithIntervalInMinutes(1).RepeatForever());
    });
});

builder.Services.AddQuartzHostedService(options =>
{
    options.WaitForJobsToComplete = true;
});

builder.Services.AddMassTransit(cfg =>
{
    cfg.SetDefaultEndpointNameFormatter();

    cfg.AddConsumer<TenantCheckedConsumer>();

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
builder.Services.AddSwaggerGen((setup)=>
{
    var commentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var commentFullPath = Path.Combine(AppContext.BaseDirectory, commentFile);

    setup.IncludeXmlComments(commentFullPath);
    setup.SwaggerDoc("RoomsOpenAPISpecification", new()
    {
        Title = "Rooms Api",
        Version = "v1",
        Description = "<h3>Rooms service created for Landlords project, provide simple interface for managing rooms</h3><br/>" +
        "Service provides endpoints for:<br/>" +
        "<ul> <li>Getting Rooms for specified apartment</li><br/>" +
        "<li>Creating new room or updating existing one</li><br/>" +
        "<li>Deleting existing room</li><br/><ul/>",
        Contact = new()
        {
            Email = "kacper.tylec1999@gmail.com",
            Name = "Kacper Tylec",
        }
    });

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI((setup) =>
    {
        setup.SwaggerEndpoint("RoomsOpenAPISpecification/swagger.json", "Rooms Api");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

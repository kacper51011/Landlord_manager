using MassTransit;
using MassTransit.DependencyInjection;
using Rooms.Application.Commands.CreateOrUpdateRoom;
using Rooms.Application.Consumers;
using Rooms.Application.Settings;
using Rooms.Domain.Interfaces;
using Rooms.Infrastructure.Repositories;
using Rooms.Infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQ"));


builder.Services.AddSingleton<IRoomsRepository, RoomsRepository>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(CreateOrUpdateRoomCommandHandler)));

builder.Services.AddMassTransit(cfg =>
{
    cfg.SetDefaultEndpointNameFormatter();

    cfg.AddConsumer<TenantCheckConsumer>();

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
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

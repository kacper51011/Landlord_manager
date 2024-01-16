using Apartments.API.Configurations;
using Apartments.API.Registers;
using Apartments.Application.Commands.CreateOrUpdateApartment;
using Apartments.Application.Consumers;
using Apartments.Application.Consumers.Statistics;
using Apartments.Application.Settings;
using Apartments.Application.Workers;
using Apartments.Domain.Interfaces;
using Apartments.Infrastructure.Db;
using Apartments.Infrastructure.Repositories;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureOptions<MongoSettingsSetup>();
builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQ"));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(CreateOrUpdateApartmentCommandHandler)));

builder.Services.AddSingleton<IApartmentsRepository, ApartmentsRepository>();
builder.Services.AddSingleton<IApartmentsStatisticsRepository, ApartmentsStatisticsRepository>();

builder.Services.AddQuartz(options =>
{
    var jobKey = JobKey.Create(nameof(SendStatisticsBackgroundJob));

    options
    .AddJob<SendStatisticsBackgroundJob>(jobKey)
    .AddTrigger(trigger =>
    {
        trigger.ForJob(jobKey).WithSimpleSchedule(schedule => schedule.WithIntervalInMinutes(10).RepeatForever());
    });

    var jobKey2 = JobKey.Create(nameof(StartGettingStatisticsBackgroundJob));

    options
    .AddJob<StartGettingStatisticsBackgroundJob>(jobKey2)
    .AddTrigger(trigger =>
    {
        trigger.ForJob(jobKey2).WithSimpleSchedule(schedule => schedule.WithIntervalInMinutes(5).RepeatForever());
    });


});

builder.Services.AddMassTransit(cfg =>
{
    cfg.SetDefaultEndpointNameFormatter();

    cfg.AddConsumer<ApartmentsHourStatisticsMessageConsumer>();
    cfg.AddConsumer<ApartmentsDayStatisticsMessageConsumer>();
    cfg.AddConsumer<ApartmentsMonthStatisticsMessageConsumer>();
    cfg.AddConsumer<ApartmentsYearStatisticsMessageConsumer>();


    cfg.AddConsumer<ManuallyCreatedStatisticMessageConsumer>();
    cfg.AddConsumer<ApartmentStatisticsToProcessMessageConsumer>();

    

    cfg.AddConsumer<RoomCheckedConsumer>();


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

builder.Services.AddVersioning(builder.Configuration);

builder.Services.AddSwagger(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

        foreach (var description in provider.ApiVersionDescriptions)
        { 
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.ApiVersion.ToString());
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

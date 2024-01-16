using MassTransit;
using Quartz;
using Statistics.Application.Commands.Apartments.CreateDayStatistics;
using Statistics.Application.Consumers.Apartments;
using Statistics.Application.Consumers.Rooms;
using Statistics.Application.Consumers.Tenants;
using Statistics.Application.Settings;
using Statistics.Application.Workers.InitializationJobs;
using Statistics.Domain.Interfaces;
using Statistics.Infrastructure.Repositories;
using Statistics.Infrastructure.Settings;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQ"));

builder.Services.AddSingleton<IApartmentsStatisticsRepository, ApartmentsStatisticsRepository>();
builder.Services.AddSingleton<IRoomsStatisticsRepository, RoomsStatisticsRepository>();
builder.Services.AddSingleton<ITenantsStatisticsRepository, TenantsStatisticsRepository>();

builder.Services.AddQuartz(options =>
{
    var hourJob = JobKey.Create(nameof(HourStatisticsJob));
    var dayJob = JobKey.Create(nameof(DayStatisticsJob));
    var monthJob = JobKey.Create(nameof(MonthStatisticsJob));
    var yearJob = JobKey.Create(nameof(YearStatisticsJob));

    //options
    options
    .AddJob<HourStatisticsJob>(hourJob)
    .AddTrigger(trigger =>
    {
        trigger.ForJob(hourJob).WithCronSchedule(CronScheduleBuilder.CronSchedule("0 0 * 1/1 * ? *"));

    })
    .AddJob<DayStatisticsJob>(dayJob)
    .AddTrigger(trigger =>
    {
        trigger.ForJob(dayJob).WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(0, 1));
    })
    .AddJob<MonthStatisticsJob>(monthJob)
    .AddTrigger(trigger =>
    {
        //trigger.ForJob(monthJob).WithCronSchedule("0 0 0 1 * ? *");
        trigger.ForJob(monthJob).WithSchedule(CronScheduleBuilder.MonthlyOnDayAndHourAndMinute(1, 0, 1));

    })
    .AddJob<YearStatisticsJob>(yearJob)
    .AddTrigger(trigger =>
    {
        trigger.ForJob(yearJob).WithCronSchedule("0 0 0 1 JAN ? *");
    });
});


builder.Services.AddMassTransit(cfg =>
{
    cfg.SetDefaultEndpointNameFormatter();

    //apartments consumers
    cfg.AddConsumer<ApartmentsHourStatisticsMessageConsumer>();
    cfg.AddConsumer<ApartmentsDayStatisticsMessageConsumer>();
    cfg.AddConsumer<ApartmentsMonthStatisticsMessageConsumer>();
    cfg.AddConsumer<ApartmentsYearStatisticsMessageConsumer>();
    cfg.AddConsumer<ApartmentsStatisticsResultMessageConsumer>();

    //rooms consumers
    cfg.AddConsumer<RoomsHourStatisticsMessageConsumer>();
    cfg.AddConsumer<RoomsDayStatisticsMessageConsumer>();
    cfg.AddConsumer<RoomsMonthStatisticsMessageConsumer>();
    cfg.AddConsumer<RoomsYearStatisticsMessageConsumer>();
    cfg.AddConsumer<RoomsStatisticsResultMessageConsumer>();

    //tenants consumers
    cfg.AddConsumer<TenantsHourStatisticsMessageConsumer>();
    cfg.AddConsumer<TenantsDayStatisticsMessageConsumer>();
    cfg.AddConsumer<TenantsMonthStatisticsMessageConsumer>();
    cfg.AddConsumer<TenantsYearStatisticsMessageConsumer>();
    cfg.AddConsumer<TenantsStatisticsResultMessageConsumer>();

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

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(CreateApartmentDayStatisticsCommand)));


builder.Services.AddQuartzHostedService(options =>
{
    options.WaitForJobsToComplete = true;
    options.AwaitApplicationStarted = true;
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen((setup) =>
{
    var commentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var commentFullPath = Path.Combine(AppContext.BaseDirectory, commentFile);

    setup.IncludeXmlComments(commentFullPath);
    setup.SwaggerDoc("StatisticsOpenAPISpecification", new()
    {
        Title = "Statistics Api",
        Version = "v1",
        Description = "<h3>Statistics service created for Landlords project, provide simple interface for managing statistics across other services</h3><br/>" +
        "Service provides endpoints for:<br/>" +
        "<ul> <li>Getting statistics for specified time interval for chosen type of data</li><br/>" +
        "<li>Creating new statistic</li><br/>",
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
        setup.SwaggerEndpoint("StatisticsOpenAPISpecification/swagger.json", "Rooms Api");
    });
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

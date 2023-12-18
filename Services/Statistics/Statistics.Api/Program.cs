using Quartz;
using Statistics.Application.Settings;
using Statistics.Application.Workers;

using Statistics.Infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQ"));

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
        trigger.ForJob(hourJob).WithCronSchedule("0 * * * *");
    })
    .AddJob<DayStatisticsJob>(dayJob)
    .AddTrigger(trigger =>
    {
        trigger.ForJob(dayJob).WithCronSchedule("0 0 0 ? * * *");
    })
    .AddJob<MonthStatisticsJob>(monthJob)
    .AddTrigger(trigger =>
    {
        trigger.ForJob(monthJob).WithCronSchedule("0 0 0 1 * ? *");
    })
    .AddJob<YearStatisticsJob>(yearJob)
    .AddTrigger(trigger =>
    {
        trigger.ForJob(yearJob).WithCronSchedule("0 0 0 1 JAN ? *");
    });
});

builder.Services.AddQuartzHostedService(options =>
{
    options.WaitForJobsToComplete = true;
    options.AwaitApplicationStarted = true;
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

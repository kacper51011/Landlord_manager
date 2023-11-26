using Quartz;
using Statistics.Application.Settings;
using Statistics.Infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQ"));

builder.Services.AddQuartz(options =>
{
    //var jobKey = JobKey.Create(nameof(RoomCheckBackgroundJob));

    //options
    //.AddJob<RoomCheckBackgroundJob>(jobKey)
    //.AddTrigger(trigger =>
    //{
    //    trigger.ForJob(jobKey).WithSimpleSchedule(schedule => schedule.WithIntervalInMinutes(2).RepeatForever());
    //});
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

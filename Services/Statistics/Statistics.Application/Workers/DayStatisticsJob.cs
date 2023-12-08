using Contracts.StatisticsMessages;
using MassTransit;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Application.Workers
{
    public class DayStatisticsJob: IJob
    {
        private readonly ILogger<DayStatisticsJob> _logger;
        private readonly IPublishEndpoint _publishEndpoint;
        public DayStatisticsJob(ILogger<DayStatisticsJob> logger, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;

        }
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                //gets perfect values from planned hour of job start, no matter how much time later it actually starts
                var date = context.ScheduledFireTimeUtc.Value;
                await _publishEndpoint.Publish(new StatisticDayMessage { Year = date.Year,Month = date.Month, Day = date.Day });
                _logger.LogInformation($"Day message send for {date}");
                return;
            }
            catch (Exception)
            {
                _logger.LogError($"Something went wrong when sending Day message for {context.ScheduledFireTimeUtc.Value}");
                throw;
            }
        }
    }
}

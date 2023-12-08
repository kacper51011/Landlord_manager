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
    public class YearStatisticsJob: IJob
    {
        private readonly ILogger<YearStatisticsJob> _logger;
        private readonly IPublishEndpoint _publishEndpoint;
        public YearStatisticsJob(ILogger<YearStatisticsJob> logger, IPublishEndpoint publishEndpoint)
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
                await _publishEndpoint.Publish(new StatisticYearMessage { Year = date.Year });
                _logger.LogInformation($"Year message send for {date}");
                return;
            }
            catch (Exception)
            {
                _logger.LogError($"Something went wrong when sending Year message for {context.ScheduledFireTimeUtc.Value}");
                throw;
            }
        }
    }
}

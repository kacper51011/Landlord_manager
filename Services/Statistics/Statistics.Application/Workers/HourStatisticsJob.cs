using Contracts.StatisticsMessages;
using MassTransit;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Statistics.Application.Workers
{
    public class HourStatisticsJob : IJob
    {
        private readonly ILogger<HourStatisticsJob> _logger;
        private readonly IPublishEndpoint _publishEndpoint;
        public HourStatisticsJob(ILogger<HourStatisticsJob> logger)
        {
            _logger = logger;
            
        }
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                //gets perfect values from planned hour of job start, no matter how much time later it actually starts
                var date = context.ScheduledFireTimeUtc.Value;
                await _publishEndpoint.Publish(new StatisticHourMessage { Year= date.Year, Day=date.Day, Month= date.Month, Hour= date.Hour});
                _logger.LogInformation($"Hour message send for {date}");
                return;
            }
            catch (Exception)
            {
                _logger.LogError($"Something went wrong when sending Hour message for {date}");
                throw;
            }
        }
    }
}

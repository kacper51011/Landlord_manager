using Contracts.StatisticsMessages.Apartments;
using MassTransit;
using Microsoft.Extensions.Logging;
using Quartz;
using Statistics.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Application.Workers.SendingJobs
{
    public class SendToApartmentsJob : IJob
    {
        private readonly IPublishEndpoint _publisher;
        private readonly IApartmentsStatisticsRepository _repository;
        private readonly ILogger<SendToApartmentsJob> _logger;
        public SendToApartmentsJob(IApartmentsStatisticsRepository repository, ILogger<SendToApartmentsJob> logger)
        {
            _repository = repository;
            _logger = logger;
            
        }
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var notSentStatistic = await _repository.GetNotSentApartmentStatistics();
                if (notSentStatistic == null)
                {
                    _logger.LogInformation("No Apartment statistic to send");
                    return;
                }
                await _publisher.Publish(new ManuallyCreatedApartmentStatisticsMessage { Hour = notSentStatistic.Hour.Value, Day = notSentStatistic.Day.Value, Month = notSentStatistic.Month.Value, Year = notSentStatistic.Year.Value });
                notSentStatistic.SetIsSent(true);
                await _repository.CreateOrUpdateApartmentStatistics(notSentStatistic);
                _logger.LogInformation($"Sent ManuallyCreatedApartmentStatisticsMessage for statistic with Id {notSentStatistic.ApartmentsStatisticsId}");

            }
            catch (Exception)
            {
                _logger.LogWarning($"Something went wrong in ManuallyCreatedApartmentStatisticsMessage");
                throw;
            }
        }
    }
}

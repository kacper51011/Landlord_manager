using Contracts.StatisticsMessages.Tenants;
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
    public class SendToTenantsJob: IJob
    {
        private readonly IPublishEndpoint _publisher;
        private readonly ITenantsStatisticsRepository _repository;
        private readonly ILogger<SendToTenantsJob> _logger;
        public SendToTenantsJob(ITenantsStatisticsRepository repository, ILogger<SendToTenantsJob> logger, IPublishEndpoint publisher)
        {
            _repository = repository;
            _logger = logger;
            _publisher = publisher;
            

        }
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var notSentStatistic = await _repository.GetNotSentTenantStatistics();
                if (notSentStatistic == null)
                {
                    _logger.LogInformation("No Tenant statistic to send");
                    return;
                }
                await _publisher.Publish(new ManuallyCreatedTenantStatisticsMessage { Hour = notSentStatistic.Hour.Value, Day = notSentStatistic.Day.Value, Month = notSentStatistic.Month.Value, Year = notSentStatistic.Year.Value });
                notSentStatistic.SetIsSent(true);
                await _repository.CreateOrUpdateTenantsStatistics(notSentStatistic);
                _logger.LogInformation($"Sent ManuallyCreatedTenantStatisticsMessage for statistic with Id {notSentStatistic.TenantStatisticId}");

            }
            catch (Exception)
            {
                _logger.LogWarning($"Something went wrong in ManuallyCreatedTenantStatisticsMessage");
                throw;
            }
        }
    }
}

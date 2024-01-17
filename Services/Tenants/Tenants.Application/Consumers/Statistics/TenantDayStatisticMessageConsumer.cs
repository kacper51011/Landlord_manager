using Contracts.StatisticsMessages;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenants.Domain;
using Tenants.Domain.Interfaces;

namespace Tenants.Application.Consumers.Statistics
{
    public class TenantsDayStatisticsMessageConsumer : IConsumer<StatisticDayMessage>
    {
        private readonly ILogger<TenantsDayStatisticsMessageConsumer> _logger;
        private readonly ITenantsStatisticsRepository _tenantsStatisticsRepository;
        public TenantsDayStatisticsMessageConsumer(ILogger<TenantsDayStatisticsMessageConsumer> logger, ITenantsStatisticsRepository tenantsStatisticsRepository)
        {
            _logger = logger;
            _tenantsStatisticsRepository = tenantsStatisticsRepository;

        }
        public async Task Consume(ConsumeContext<StatisticDayMessage> context)
        {
            try
            {
                var response = await _tenantsStatisticsRepository.GetTenantsAnyStatistics(context.Message.Year, context.Message.Month, context.Message.Day, null);
                if (response != null)
                {
                    throw new DuplicateNameException("Statistic already created");
                }
                TenantsStatistics apartmentStatistic = TenantsStatistics.CreateAsDayStatisticsInformations(context.Message.Year, context.Message.Month, context.Message.Day);
                await _tenantsStatisticsRepository.CreateOrUpdateTenantsStatistics(apartmentStatistic);
                _logger.LogInformation($"Day apartment statistic created with Id {apartmentStatistic.TenantsStatisticsId}");
                return;
            }
            catch (DuplicateNameException ex)
            {
                _logger.LogWarning(400, ex, ex.Message);
            }
            catch (Exception ex)
            {

                _logger.LogWarning(500, ex, "TenantsDayStatisticsMessageConsumer failed");
            }
        }
    }
}

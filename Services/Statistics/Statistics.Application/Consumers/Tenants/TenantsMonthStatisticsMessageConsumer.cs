using Contracts.StatisticsMessages;
using MassTransit;
using Microsoft.Extensions.Logging;
using Statistics.Domain.Entities;
using Statistics.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Application.Consumers.Tenants
{
    public class TenantsMonthStatisticsMessageConsumer : IConsumer<StatisticMonthMessage>
    {
        private readonly ILogger<TenantsMonthStatisticsMessageConsumer> _logger;
        private readonly ITenantsStatisticsRepository _tenantsStatisticsRepository;
        public TenantsMonthStatisticsMessageConsumer(ILogger<TenantsMonthStatisticsMessageConsumer> logger, ITenantsStatisticsRepository tenantsStatisticsRepository)
        {
            _logger = logger;
            _tenantsStatisticsRepository = tenantsStatisticsRepository;

        }
        public async Task Consume(ConsumeContext<StatisticMonthMessage> context)
        {
            try
            {
                var response = await _tenantsStatisticsRepository.GetTenantsMonthStatistics(context.Message.Year, context.Message.Month);
                if (response != null)
                {
                    throw new DuplicateNameException("Statistic already created");
                }
                TenantsStatistics apartmentStatistic = TenantsStatistics.CreateAsMonthStatisticsInformations(context.Message.Year, context.Message.Month, true);
                await _tenantsStatisticsRepository.CreateOrUpdateTenantsStatistics(apartmentStatistic);
                _logger.LogInformation($"Month apartment statistic created with Id {apartmentStatistic.TenantsStatisticsId}");
                return;
            }
            catch (DuplicateNameException ex)
            {
                _logger.LogWarning(400, ex, ex.Message);
            }
            catch (Exception ex)
            {

                _logger.LogWarning(500, ex, "TenantsMonthStatisticsMessageConsumer failed");
            }
        }
    }
}

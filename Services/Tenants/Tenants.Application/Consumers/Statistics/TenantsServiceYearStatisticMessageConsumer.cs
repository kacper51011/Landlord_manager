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
    public class TenantsServiceYearStatisticsMessageConsumer : IConsumer<StatisticYearMessage>
    {
        private readonly ILogger<TenantsServiceYearStatisticsMessageConsumer> _logger;
        private readonly ITenantsStatisticsRepository _tenantsStatisticsRepository;
        public TenantsServiceYearStatisticsMessageConsumer(ILogger<TenantsServiceYearStatisticsMessageConsumer> logger, ITenantsStatisticsRepository tenantsStatisticsRepository)
        {
            _logger = logger;
            _tenantsStatisticsRepository = tenantsStatisticsRepository;

        }
        public async Task Consume(ConsumeContext<StatisticYearMessage> context)
        {
            try
            {
                var response = await _tenantsStatisticsRepository.GetTenantsAnyStatistics(context.Message.Year, null, null, null);
                if (response != null)
                {
                    throw new DuplicateNameException("Statistic already created");
                }
                TenantsStatistics tenantsStatistic = TenantsStatistics.CreateAsYearStatisticsInformations(context.Message.Year);
                await _tenantsStatisticsRepository.CreateOrUpdateTenantsStatistics(tenantsStatistic);
                _logger.LogInformation($"Year tenants statistic created with Id {tenantsStatistic.TenantsStatisticsId}");
                return;
            }
            catch (DuplicateNameException ex)
            {
                _logger.LogWarning(400, ex, ex.Message);
            }
            catch (Exception ex)
            {

                _logger.LogWarning(500, ex, "TenantsServiceYearStatisticsMessageConsumer failed");
            }
        }
    }
}

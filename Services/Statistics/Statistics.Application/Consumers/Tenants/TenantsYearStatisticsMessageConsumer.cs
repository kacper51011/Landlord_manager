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
    public class TenantsYearStatisticsMessageConsumer : IConsumer<StatisticYearMessage>
    {
        private readonly ILogger<TenantsYearStatisticsMessageConsumer> _logger;
        private readonly ITenantsStatisticsRepository _tenantsStatisticsRepository;
        public TenantsYearStatisticsMessageConsumer(ILogger<TenantsYearStatisticsMessageConsumer> logger, ITenantsStatisticsRepository tenantsStatisticsRepository)
        {
            _logger = logger;
            _tenantsStatisticsRepository = tenantsStatisticsRepository;

        }
        public async Task Consume(ConsumeContext<StatisticYearMessage> context)
        {
            try
            {
                var response = await _tenantsStatisticsRepository.GetTenantsYearStatistics(context.Message.Year);
                if (response != null)
                {
                    throw new DuplicateNameException("Statistic already created");
                }
                TenantsStatistics apartmentStatistic = TenantsStatistics.CreateAsYearStatisticsInformations(context.Message.Year);
                await _tenantsStatisticsRepository.CreateOrUpdateTenantsStatistics(apartmentStatistic);
                _logger.LogInformation($"Year apartment statistic created with Id {apartmentStatistic.TenantsStatisticsId}");
                return;
            }
            catch (DuplicateNameException ex)
            {
                _logger.LogWarning(400, ex, ex.Message);
            }
            catch (Exception ex)
            {

                _logger.LogWarning(500, ex, "TenantsYearStatisticsMessageConsumer failed");
            }
        }
    }
}

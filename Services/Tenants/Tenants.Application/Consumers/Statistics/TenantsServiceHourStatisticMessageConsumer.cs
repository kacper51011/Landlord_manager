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
    public class TenantsServiceHourStatisticMessageConsumer: IConsumer<StatisticHourMessage>
    {
        private readonly ILogger<TenantsServiceHourStatisticMessageConsumer> _logger;
        private readonly ITenantsStatisticsRepository _tenantsStatisticsRepository;
        public TenantsServiceHourStatisticMessageConsumer(ILogger<TenantsServiceHourStatisticMessageConsumer> logger, ITenantsStatisticsRepository tenantsStatisticsRepository)
        {
            _logger = logger;
            _tenantsStatisticsRepository = tenantsStatisticsRepository;

        }
        public async Task Consume(ConsumeContext<StatisticHourMessage> context)
        {
            try
            {
                var response = await _tenantsStatisticsRepository.GetTenantsAnyStatistics(context.Message.Year, context.Message.Month, context.Message.Day, context.Message.Hour);
                if (response != null)
                {
                    throw new DuplicateNameException("Statistic already created");
                }
                TenantsStatistics apartmentStatistic = TenantsStatistics.CreateAsHourStatisticsInformations(context.Message.Year, context.Message.Month, context.Message.Day, context.Message.Hour);
                await _tenantsStatisticsRepository.CreateOrUpdateTenantsStatistics(apartmentStatistic);
                _logger.LogInformation($"Hour apartment statistic created with Id {apartmentStatistic.TenantsStatisticsId}");
                return;
            }
            catch (DuplicateNameException ex)
            {
                _logger.LogWarning(400, ex, ex.Message);
            }
            catch (Exception ex)
            {

                _logger.LogWarning(500, ex, "TenantsHourStatisticsMessageConsumer failed");
            }
        }
    }
}


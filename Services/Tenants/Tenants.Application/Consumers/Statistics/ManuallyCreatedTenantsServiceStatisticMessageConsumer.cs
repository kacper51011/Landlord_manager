using Contracts.StatisticsMessages.Tenants;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenants.Domain;
using Tenants.Domain.Interfaces;

namespace Tenants.Application.Consumers.Statistics
{
    public class ManuallyCreatedTenantsServiceStatisticMessageConsumer : IConsumer<ManuallyCreatedTenantStatisticsMessage>
    {
        private readonly ITenantsStatisticsRepository _statisticsRepository;
        private readonly ILogger<ManuallyCreatedTenantsServiceStatisticMessageConsumer> _logger;
        public ManuallyCreatedTenantsServiceStatisticMessageConsumer(ITenantsStatisticsRepository statisticsRepository, ILogger<ManuallyCreatedTenantsServiceStatisticMessageConsumer> logger)
        {
            _statisticsRepository = statisticsRepository;
            _logger = logger;

        }
        public async Task Consume(ConsumeContext<ManuallyCreatedTenantStatisticsMessage> context)
        {
            try
            {
                var statistic = await _statisticsRepository.GetTenantsAnyStatistics(context.Message.Year, context.Message.Month, context.Message.Day, context.Message.Hour);
                if (statistic != null)
                {
                    //If we did get this message while our statistic is not null, it means that our statistic could be lost
                    _logger.LogInformation($"Tenant Statistic already created but requested by StatisticsService");
                    statistic.SetIsSentToStatisticsService(false);
                    await _statisticsRepository.CreateOrUpdateTenantsStatistics(statistic);
                    return;
                }
                TenantsStatistics roomsStatistics;
                //a bit other strategy compared to how i handled types of statistics, but i think it was better to handle it separately in cqrs

                //create as hour
                if (context.Message.Hour.HasValue && context.Message.Month.HasValue && context.Message.Day.HasValue)
                {
                    roomsStatistics = TenantsStatistics.CreateAsHourStatisticsInformations(context.Message.Year, context.Message.Month.Value, context.Message.Day.Value, context.Message.Hour.Value);
                }

                //create as day
                else if (context.Message.Hour == null && context.Message.Month.HasValue && context.Message.Day.HasValue)
                {
                    roomsStatistics = TenantsStatistics.CreateAsDayStatisticsInformations(context.Message.Year, context.Message.Month.Value, context.Message.Day.Value);
                }

                //create as month
                else if (context.Message.Day == null && context.Message.Hour == null && context.Message.Month.HasValue)
                {
                    roomsStatistics = TenantsStatistics.CreateAsMonthStatisticsInformations(context.Message.Year, context.Message.Month.Value);
                }

                //create as year
                else
                {
                    roomsStatistics = TenantsStatistics.CreateAsYearStatisticsInformations(context.Message.Year);
                }

                await _statisticsRepository.CreateOrUpdateTenantsStatistics(roomsStatistics);
            }
            catch (Exception)
            {
                _logger.LogWarning("Something went wrong in ManuallyCreatedStatisticMessageConsumer in apartments service");
                throw;
            }

        }
    }
}

using Contracts.StatisticsMessages.Tenants;
using MassTransit;
using Microsoft.Extensions.Logging;
using Statistics.Domain.Entities;
using Statistics.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Application.Consumers.Tenants
{
    public class TenantsStatisticsResultMessageConsumer : IConsumer<TenantsStatisticsResultMessage>
    {
        private readonly ILogger<TenantsStatisticsResultMessageConsumer> _logger;
        private readonly ITenantsStatisticsRepository _tenantsStatisticsRepository;
        public TenantsStatisticsResultMessageConsumer(ILogger<TenantsStatisticsResultMessageConsumer> logger, ITenantsStatisticsRepository tenantsStatisticsRepository)
        {
            _logger = logger;
            _tenantsStatisticsRepository = tenantsStatisticsRepository;

        }
        public async Task Consume(ConsumeContext<TenantsStatisticsResultMessage> context)
        {
            try
            {
                var statistic = await _tenantsStatisticsRepository.GetTenantsAnyStatistics(context.Message.Year, context.Message.Month, context.Message.Day, context.Message.Hour);
                if (statistic == null)
                {
                    TenantsStatistics tenantsStatistics;
                    //a bit other strategy compared to how i handled types of statistics, but i think it was better to handle it separately in cqrs

                    //create as hour
                    if (context.Message.Hour.HasValue && context.Message.Month.HasValue && context.Message.Day.HasValue)
                    {
                        tenantsStatistics = TenantsStatistics.CreateAsHourStatisticsInformations(context.Message.Year, context.Message.Month.Value, context.Message.Day.Value, context.Message.Hour.Value, true);
                    }

                    //create as day
                    if (context.Message.Hour == null && context.Message.Month.HasValue && context.Message.Day.HasValue)
                    {
                        tenantsStatistics = TenantsStatistics.CreateAsDayStatisticsInformations(context.Message.Year, context.Message.Month.Value, context.Message.Day.Value);
                    }

                    //create as month
                    if (context.Message.Day == null && context.Message.Hour == null && context.Message.Month.HasValue)
                    {
                        tenantsStatistics = TenantsStatistics.CreateAsMonthStatisticsInformations(context.Message.Year, context.Message.Month.Value);
                    }

                    //create as year
                    else
                    {
                        tenantsStatistics = TenantsStatistics.CreateAsYearStatisticsInformations(context.Message.Year);
                    }

                    tenantsStatistics.SetStatistics(context.Message.TenantsCreated, context.Message.TenantsUpdated, context.Message.MostTenantsInRoom, context.Message.HighestRent);
                    await _tenantsStatisticsRepository.CreateOrUpdateTenantsStatistics(statistic);
                    _logger.LogInformation($"created new statistic from consumer, statistic with id {statistic.TenantsStatisticsId}");
                    return;

                }


                statistic.SetStatistics(context.Message.TenantsCreated, context.Message.TenantsUpdated, context.Message.MostTenantsInRoom, context.Message.HighestRent);
                await _tenantsStatisticsRepository.CreateOrUpdateTenantsStatistics(statistic);
                _logger.LogInformation($"updated statistic with id {statistic.TenantsStatisticsId}");
            }

            catch (Exception ex)
            {
                _logger.LogError($"Updating with result failed");

                throw ex;
            }

        }
    }
}

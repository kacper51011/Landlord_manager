﻿using Apartments.Domain;
using Apartments.Domain.Interfaces;
using Contracts.StatisticsMessages;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Data;

namespace Apartments.Application.Consumers.Statistics
{
    public class ApartmentsServiceMonthStatisticsMessageConsumer : IConsumer<StatisticMonthMessage>
    {
        private readonly ILogger<ApartmentsServiceMonthStatisticsMessageConsumer> _logger;
        private readonly IApartmentsStatisticsRepository _apartmentsStatisticsRepository;
        public ApartmentsServiceMonthStatisticsMessageConsumer(ILogger<ApartmentsServiceMonthStatisticsMessageConsumer> logger, IApartmentsStatisticsRepository apartmentsStatisticsRepository)
        {
            _logger = logger;
            _apartmentsStatisticsRepository = apartmentsStatisticsRepository;

        }
        public async Task Consume(ConsumeContext<StatisticMonthMessage> context)
        {
            try
            {
                var response = await _apartmentsStatisticsRepository.GetApartmentAnyStatistics(context.Message.Year, context.Message.Month, null, null);
                if (response != null)
                {
                    throw new DuplicateNameException("Statistic already created");
                }
                ApartmentsStatistics apartmentStatistic = ApartmentsStatistics.CreateAsMonthStatisticsInformations(context.Message.Year, context.Message.Month);
                await _apartmentsStatisticsRepository.CreateOrUpdateApartmentStatistics(apartmentStatistic);
                _logger.LogInformation($"Month apartment statistic created with Id {apartmentStatistic.ApartmentsStatisticsId}");
                return;
            }
            catch (DuplicateNameException ex)
            {
                _logger.LogWarning(400, ex, ex.Message);
            }
            catch (Exception ex)
            {

                _logger.LogWarning(500, ex, "ApartmentsMonthStatisticsMessageConsumer failed");
            }
        }
    }
}

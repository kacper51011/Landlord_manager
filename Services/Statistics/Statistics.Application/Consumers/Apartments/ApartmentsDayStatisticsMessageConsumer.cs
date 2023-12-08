using Contracts.StatisticsMessages;
using MassTransit;
using Microsoft.Extensions.Logging;
using Statistics.Domain.Entities;
using Statistics.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Application.Consumers.Apartments
{
    public class ApartmentsDayStatisticsMessageConsumer : IConsumer<StatisticDayMessage>
    {
        private readonly ILogger<ApartmentsDayStatisticsMessageConsumer> _logger;
        private readonly IApartmentsStatisticsRepository _apartmentsStatisticsRepository;
        public ApartmentsDayStatisticsMessageConsumer(ILogger<ApartmentsDayStatisticsMessageConsumer> logger, IApartmentsStatisticsRepository apartmentsStatisticsRepository)
        {
            _logger = logger;
            _apartmentsStatisticsRepository = apartmentsStatisticsRepository;
            
        }
        public async Task Consume(ConsumeContext<StatisticDayMessage> context)
        {
            try
            {
                var response = await _apartmentsStatisticsRepository.GetApartmentDayStatistics(context.Message.Year, context.Message.Month, context.Message.Day);
                if (response != null)
                {
                    _logger.LogInformation($"Day apartment statistic for date {response.StatisticsStart} was already created");
                    return;
                }
                ApartmentsStatistics apartmentStatistic = ApartmentsStatistics.CreateAsDayStatisticsInformations(context.Message.Year, context.Message.Month, context.Message.Day);
                await _apartmentsStatisticsRepository.CreateOrUpdateApartmentStatistics(apartmentStatistic);
                _logger.LogInformation($"Day apartment statistic created with Id {apartmentStatistic.ApartmentsStatisticsId}");
                return;
            }
            catch (Exception)
            {
                _logger.LogError($"Something went wrong in ApartmentsDayStatisticsMessageConsumer");
                throw;
            }
        }
    }
}

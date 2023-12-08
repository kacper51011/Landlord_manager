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
    public class ApartmentsYearStatisticsMessageConsumer: IConsumer<StatisticYearMessage>
    {
        private readonly ILogger<ApartmentsYearStatisticsMessageConsumer> _logger;
        private readonly IApartmentsStatisticsRepository _apartmentsStatisticsRepository;
        public ApartmentsYearStatisticsMessageConsumer(ILogger<ApartmentsYearStatisticsMessageConsumer> logger, IApartmentsStatisticsRepository apartmentsStatisticsRepository)
        {
            _logger = logger;
            _apartmentsStatisticsRepository = apartmentsStatisticsRepository;

        }
        public async Task Consume(ConsumeContext<StatisticYearMessage> context)
        {
            try
            {
                var response = await _apartmentsStatisticsRepository.GetApartmentYearStatistics(context.Message.Year);
                if (response != null)
                {
                    _logger.LogInformation($"Year apartment statistic for date {response.StatisticsStart} was already created");
                    return;
                }
                ApartmentsStatistics apartmentStatistic = ApartmentsStatistics.CreateAsYearStatisticsInformations(context.Message.Year);
                await _apartmentsStatisticsRepository.CreateOrUpdateApartmentStatistics(apartmentStatistic);
                _logger.LogInformation($"Year apartment statistic created with Id {apartmentStatistic.ApartmentsStatisticsId}");
                return;
            }
            catch (Exception)
            {
                _logger.LogError($"Something went wrong in ApartmentsYearStatisticsMessageConsumer");
                throw;
            }
        }
    }
}

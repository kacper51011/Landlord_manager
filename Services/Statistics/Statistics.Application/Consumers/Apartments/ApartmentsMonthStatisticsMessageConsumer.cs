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

namespace Statistics.Application.Consumers.Apartments
{
    public class ApartmentsMonthStatisticsMessageConsumer: IConsumer<StatisticMonthMessage>
    {
        private readonly ILogger<ApartmentsMonthStatisticsMessageConsumer> _logger;
        private readonly IApartmentsStatisticsRepository _apartmentsStatisticsRepository;
        public ApartmentsMonthStatisticsMessageConsumer(ILogger<ApartmentsMonthStatisticsMessageConsumer> logger, IApartmentsStatisticsRepository apartmentsStatisticsRepository)
        {
            _logger = logger;
            _apartmentsStatisticsRepository = apartmentsStatisticsRepository;

        }
        public async Task Consume(ConsumeContext<StatisticMonthMessage> context)
        {
            try
            {
                var response = await _apartmentsStatisticsRepository.GetApartmentMonthStatistics(context.Message.Year, context.Message.Month);
                if (response != null)
                {
                    throw new DuplicateNameException("Statistic already created");
                }
                ApartmentsStatistics apartmentStatistic = ApartmentsStatistics.CreateAsMonthStatisticsInformations(context.Message.Year, context.Message.Month , true);
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

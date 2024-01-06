using Apartments.Domain;
using Apartments.Domain.Interfaces;
using Contracts.StatisticsMessages;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apartments.Application.Consumers.Statistics
{
    public class ApartmentsHourStatisticsMessageConsumer : IConsumer<StatisticHourMessage>
    {
        private readonly ILogger<ApartmentsHourStatisticsMessageConsumer> _logger;
        private readonly IApartmentsStatisticsRepository _apartmentsStatisticsRepository;
        public ApartmentsHourStatisticsMessageConsumer(ILogger<ApartmentsHourStatisticsMessageConsumer> logger, IApartmentsStatisticsRepository apartmentsStatisticsRepository)
        {
            _logger = logger;
            _apartmentsStatisticsRepository = apartmentsStatisticsRepository;

        }
        public async Task Consume(ConsumeContext<StatisticHourMessage> context)
        {
            try
            {
                var response = await _apartmentsStatisticsRepository.GetApartmentAnyStatistics(context.Message.Year, context.Message.Month, context.Message.Day, context.Message.Hour);
                if (response != null)
                {
                    throw new DuplicateNameException("Statistic already created");
                }
                ApartmentsStatistics apartmentStatistic = ApartmentsStatistics.CreateAsHourStatisticsInformations(context.Message.Year, context.Message.Month, context.Message.Day, context.Message.Hour);
                await _apartmentsStatisticsRepository.CreateOrUpdateApartmentStatistics(apartmentStatistic);
                _logger.LogInformation($"Hour apartment statistic created with Id {apartmentStatistic.ApartmentsStatisticsId}");
                return;
            }
            catch (DuplicateNameException ex)
            {
                _logger.LogWarning(400, ex, ex.Message);
            }
            catch (Exception ex)
            {

                _logger.LogWarning(500, ex, "ApartmentsHourStatisticsMessageConsumer failed");
            }
        }
    }
}

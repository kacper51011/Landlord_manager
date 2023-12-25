using Contracts.StatisticsMessages.Apartments;
using MassTransit;
using Microsoft.Extensions.Logging;
using Statistics.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Application.Consumers.Apartments
{
    public class ApartmentsStatisticsResultMessageConsumer : IConsumer<ApartmentsStatisticsResultMessage>
    {
        private readonly ILogger<ApartmentsStatisticsResultMessageConsumer> _logger;
        private readonly IApartmentsStatisticsRepository _apartmentsStatisticsRepository;
        public ApartmentsStatisticsResultMessageConsumer(ILogger<ApartmentsStatisticsResultMessageConsumer> logger, IApartmentsStatisticsRepository apartmentsStatisticsRepository)
        {
            _logger = logger;
            _apartmentsStatisticsRepository = apartmentsStatisticsRepository;
            
        }
        public async Task Consume(ConsumeContext<ApartmentsStatisticsResultMessage> context)
        {
            try
            {
                var statistic = await _apartmentsStatisticsRepository.GetApartmentAnyStatistics(context.Message.Year, context.Message.Month, context.Message.Day, context.Message.Hour, true);
                if (statistic == null)
                {
                    throw new ArgumentNullException();
                }
                statistic.SetStatistics(context.Message.ApartmentsCreated, context.Message.ApartmentsUpdated, context.Message.MostApartmentsOwnedByUser);
                await _apartmentsStatisticsRepository.CreateOrUpdateApartmentStatistics(statistic);
                _logger.LogInformation($"updated statistic with id {statistic.ApartmentsStatisticsId}");
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Updating with result failed");

                throw ex;
            }

        }
    }
}

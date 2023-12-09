using Contracts.StatisticsMessages;
using MassTransit;
using Microsoft.Extensions.Logging;
using Statistics.Application.Consumers.Rooms;
using Statistics.Domain.Entities;
using Statistics.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Application.Consumers.Rooms
{
    public class RoomsMonthStatisticsMessageConsumer: IConsumer<StatisticMonthMessage>
    {
        private readonly ILogger<RoomsMonthStatisticsMessageConsumer> _logger;
        private readonly IRoomsStatisticsRepository _roomsStatisticsRepository;
        public RoomsMonthStatisticsMessageConsumer(ILogger<RoomsMonthStatisticsMessageConsumer> logger, IRoomsStatisticsRepository roomsStatisticsRepository)
        {
            _logger = logger;
            _roomsStatisticsRepository = roomsStatisticsRepository;

        }
        public async Task Consume(ConsumeContext<StatisticMonthMessage> context)
        {
            try
            {
                var response = await _roomsStatisticsRepository.GetRoomsMonthStatistics(context.Message.Year, context.Message.Month);
                if (response != null)
                {
                    _logger.LogInformation($"Month apartment statistic for date {response.StatisticsStart} was already created");
                    return;
                }
                RoomsStatistics apartmentStatistic = RoomsStatistics.CreateAsMonthStatisticsInformations(context.Message.Year, context.Message.Month);
                await _roomsStatisticsRepository.CreateOrUpdateRoomsStatistics(apartmentStatistic);
                _logger.LogInformation($"Month apartment statistic created with Id {apartmentStatistic.RoomsStatisticsId}");
                return;
            }
            catch (Exception)
            {
                _logger.LogError($"Something went wrong in RoomsMonthStatisticsMessageConsumer");
                throw;
            }
        }
    }
}

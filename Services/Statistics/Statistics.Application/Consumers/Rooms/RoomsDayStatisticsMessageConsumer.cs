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
    public class RoomsDayStatisticsMessageConsumer: IConsumer<StatisticDayMessage>
    {
        private readonly ILogger<RoomsDayStatisticsMessageConsumer> _logger;
        private readonly IRoomsStatisticsRepository _roomsStatisticsRepository;
        public RoomsDayStatisticsMessageConsumer(ILogger<RoomsDayStatisticsMessageConsumer> logger, IRoomsStatisticsRepository roomsStatisticsRepository)
        {
            _logger = logger;
            _roomsStatisticsRepository = roomsStatisticsRepository;

        }
        public async Task Consume(ConsumeContext<StatisticDayMessage> context)
        {
            try
            {
                var response = await _roomsStatisticsRepository.GetRoomsDayStatistics(context.Message.Year, context.Message.Month, context.Message.Day);
                if (response != null)
                {
                    _logger.LogInformation($"Day room statistic for date {response.StatisticsStart} was already created");
                    return;
                }
                RoomsStatistics roomStatistic = RoomsStatistics.CreateAsDayStatisticsInformations(context.Message.Year, context.Message.Month, context.Message.Day);
                await _roomsStatisticsRepository.CreateOrUpdateRoomsStatistics(roomStatistic);
                _logger.LogInformation($"Day room statistic created with Id {roomStatistic.RoomsStatisticsId}");
                return;
            }
            catch (Exception)
            {
                _logger.LogError($"Something went wrong in RoomsDayStatisticsMessageConsumer");
                throw;
            }
        }
    }
}

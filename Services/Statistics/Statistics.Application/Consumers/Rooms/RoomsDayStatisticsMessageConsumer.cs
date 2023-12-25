using Contracts.StatisticsMessages;
using MassTransit;
using Microsoft.Extensions.Logging;
using Statistics.Application.Consumers.Rooms;
using Statistics.Domain.Entities;
using Statistics.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
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
                    throw new DuplicateNameException("Statistic already created");
                }
                RoomsStatistics roomStatistic = RoomsStatistics.CreateAsDayStatisticsInformations(context.Message.Year, context.Message.Month, context.Message.Day, true);
                await _roomsStatisticsRepository.CreateOrUpdateRoomsStatistics(roomStatistic);
                _logger.LogInformation($"Day room statistic created with Id {roomStatistic.RoomsStatisticsId}");
                return;
            }
            catch (DuplicateNameException ex)
            {
                _logger.LogWarning(400, ex, ex.Message);
            }
            catch (Exception ex)
            {

                _logger.LogWarning(500, ex, "RoomsDayStatisticsMessageConsumer failed");
            }
        }
    }
}

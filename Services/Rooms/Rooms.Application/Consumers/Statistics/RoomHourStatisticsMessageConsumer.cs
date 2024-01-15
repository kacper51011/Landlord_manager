using Contracts.StatisticsMessages;
using MassTransit;
using Microsoft.Extensions.Logging;
using Rooms.Domain;
using Rooms.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rooms.Application.Consumers.Statistics
{
    public class RoomHourStatisticsMessageConsumer : IConsumer<StatisticHourMessage>
    {
        private readonly ILogger<RoomHourStatisticsMessageConsumer> _logger;
        private readonly IRoomsStatisticsRepository _roomsStatisticsRepository;
        public RoomHourStatisticsMessageConsumer(ILogger<RoomHourStatisticsMessageConsumer> logger, IRoomsStatisticsRepository roomsStatisticsRepository)
        {
            _logger = logger;
            _roomsStatisticsRepository = roomsStatisticsRepository;

        }
        public async Task Consume(ConsumeContext<StatisticHourMessage> context)
        {
            try
            {
                var response = await _roomsStatisticsRepository.GetRoomAnyStatistics(context.Message.Year, context.Message.Month, context.Message.Day, context.Message.Hour);
                if (response != null)
                {
                    throw new DuplicateNameException("Statistic already created");
                }
                RoomsStatistics roomStatistic = RoomsStatistics.CreateAsHourStatisticsInformations(context.Message.Year, context.Message.Month, context.Message.Day, context.Message.Hour);
                await _roomsStatisticsRepository.CreateOrUpdateRoomStatistics(roomStatistic);
                _logger.LogInformation($"Hour room statistic created with Id {roomStatistic.RoomsStatisticsId}");
                return;
            }
            catch (DuplicateNameException ex)
            {
                _logger.LogWarning(400, ex, ex.Message);
            }
            catch (Exception ex)
            {

                _logger.LogWarning(500, ex, "RoomHourStatisticsMessageConsumer failed");
            }
        }
    }
}

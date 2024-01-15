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
    public class RoomYearStatisticsMessageConsumer : IConsumer<StatisticYearMessage>
    {
        private readonly ILogger<RoomYearStatisticsMessageConsumer> _logger;
        private readonly IRoomsStatisticsRepository _roomsStatisticsRepository;
        public RoomYearStatisticsMessageConsumer(ILogger<RoomYearStatisticsMessageConsumer> logger, IRoomsStatisticsRepository roomsStatisticsRepository)
        {
            _logger = logger;
            _roomsStatisticsRepository = roomsStatisticsRepository;

        }
        public async Task Consume(ConsumeContext<StatisticYearMessage> context)
        {
            try
            {
                var response = await _roomsStatisticsRepository.GetRoomAnyStatistics(context.Message.Year, null, null, null);
                if (response != null)
                {
                    throw new DuplicateNameException("Statistic already created");
                }
                RoomsStatistics roomStatistic = RoomsStatistics.CreateAsYearStatisticsInformations(context.Message.Year);
                await _roomsStatisticsRepository.CreateOrUpdateRoomStatistics(roomStatistic);
                _logger.LogInformation($"Year room statistic created with Id {roomStatistic.RoomsStatisticsId}");
                return;
            }
            catch (DuplicateNameException ex)
            {
                _logger.LogWarning(400, ex, ex.Message);
            }
            catch (Exception ex)
            {

                _logger.LogWarning(500, ex, "RoomYearStatisticsMessageConsumer failed");
            }
        }
    }
}

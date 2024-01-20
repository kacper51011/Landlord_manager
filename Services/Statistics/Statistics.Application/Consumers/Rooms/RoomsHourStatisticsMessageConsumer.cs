using Contracts.StatisticsMessages;
using MassTransit;
using Microsoft.Extensions.Logging;
using Statistics.Domain.Entities;
using Statistics.Domain.Interfaces;
using System.Data;

namespace Statistics.Application.Consumers.Rooms
{
    public class RoomsHourStatisticsMessageConsumer : IConsumer<StatisticHourMessage>
    {
        private readonly ILogger<RoomsHourStatisticsMessageConsumer> _logger;
        private readonly IRoomsStatisticsRepository _roomsStatisticsRepository;
        public RoomsHourStatisticsMessageConsumer(ILogger<RoomsHourStatisticsMessageConsumer> logger, IRoomsStatisticsRepository roomsStatisticsRepository)
        {
            _logger = logger;
            _roomsStatisticsRepository = roomsStatisticsRepository;

        }
        public async Task Consume(ConsumeContext<StatisticHourMessage> context)
        {
            try
            {
                var response = await _roomsStatisticsRepository.GetRoomsHourStatistics(context.Message.Year, context.Message.Month, context.Message.Day, context.Message.Hour);
                if (response == null)
                {
                    RoomsStatistics roomStatistic = RoomsStatistics.CreateAsHourStatisticsInformations(context.Message.Year, context.Message.Month, context.Message.Day, context.Message.Hour, true);
                    await _roomsStatisticsRepository.CreateOrUpdateRoomsStatistics(roomStatistic);
                    _logger.LogInformation($"Hour room statistic created with Id {roomStatistic.RoomsStatisticsId}");
                }
                else
                {
                    throw new DuplicateNameException("Statistic already created");
                }


                return;
            }
            catch (DuplicateNameException ex)
            {
                _logger.LogWarning(400, ex, ex.Message);
            }
            catch (Exception ex)
            {

                _logger.LogWarning(500, ex, "RoomsHourStatisticsMessageConsumer failed");
            }
        }
    }
}

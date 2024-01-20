using Contracts.StatisticsMessages.Rooms;
using MassTransit;
using Microsoft.Extensions.Logging;
using Quartz;
using Rooms.Domain.Interfaces;

namespace Rooms.Application.Worker
{
    public class SendStatisticsBackgroundJob : IJob
    {

        private readonly IRoomsStatisticsRepository _roomsStatisticsRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<SendStatisticsBackgroundJob> _logger;

        public SendStatisticsBackgroundJob(IRoomsStatisticsRepository roomsStatisticsRepository, IPublishEndpoint publishEndpoint, ILogger<SendStatisticsBackgroundJob> logger)
        {
            _roomsStatisticsRepository = roomsStatisticsRepository;
            _publishEndpoint = publishEndpoint;
            _logger = logger;

        }
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                //atomically finds and updates the statistic
                var notSendStatistic = await _roomsStatisticsRepository.GetNotSendRoomsStatistics();
                if (notSendStatistic == null)
                {
                    _logger.LogInformation("No statistic to send in rooms service");
                    return;
                }
                var statisticResultMessage = new RoomsStatisticsResultMessage
                {
                    RoomsCreated = notSendStatistic.RoomsCreated,
                    RoomsUpdated = notSendStatistic.RoomsUpdated,
                    MostRoomsInApartment = notSendStatistic.MostRoomsInApartment,
                    BiggestCreatedRoomSize = notSendStatistic.BiggestCreatedRoomSize,
                    Year = notSendStatistic.Year.Value,
                    Month = notSendStatistic.Month.Value,
                    Day = notSendStatistic.Day.Value,
                    Hour = notSendStatistic.Hour.Value
                };
                await _publishEndpoint.Publish(statisticResultMessage);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Something went wrong in SendStatisticsBackgroundJob in roomsService");
                throw;
            }




        }
    }

}

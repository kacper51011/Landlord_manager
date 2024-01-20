using Contracts.RoomsService.RoomsStatistics;
using MassTransit;
using Microsoft.Extensions.Logging;
using Rooms.Domain.Interfaces;

namespace Rooms.Application.Consumers.Statistics
{
    public class RoomServiceStatisticsToProcessMessageConsumer : IConsumer<RoomStatisticToProcessMessage>
    {
        private readonly IRoomsRepository _roomsReposistory;
        private readonly IRoomsStatisticsRepository _statisticsRepository;
        private readonly ILogger<RoomServiceStatisticsToProcessMessageConsumer> _logger;
        public RoomServiceStatisticsToProcessMessageConsumer(IRoomsRepository roomsRepository, IRoomsStatisticsRepository statisticsRepository, ILogger<RoomServiceStatisticsToProcessMessageConsumer> logger)
        {
            _roomsReposistory = roomsRepository;
            _statisticsRepository = statisticsRepository;
            _logger = logger;

        }
        public async Task Consume(ConsumeContext<RoomStatisticToProcessMessage> context)
        {
            try
            {
                var statisticToProcess = await _statisticsRepository.GetRoomStatisticsById(context.Message.RoomStatisticId);
                if (statisticToProcess == null)
                {
                    throw new ArgumentNullException();
                }

                var createdRoomsCount = await _roomsReposistory.GetCreatedRoomsCount(statisticToProcess.StatisticsStart.Value, statisticToProcess.StatisticsEnd.Value);
                var updatedRoomsCount = await _roomsReposistory.GetUpdatedRoomsCount(statisticToProcess.StatisticsStart.Value, statisticToProcess.StatisticsEnd.Value);
                var biggestRoomSize = await _roomsReposistory.GetBiggestCreatedRoomSize(statisticToProcess.StatisticsEnd.Value);
                var mostRoomsInApartmentCount = await _roomsReposistory.GetMostRoomsInOneApartment(statisticToProcess.StatisticsEnd.Value);

                statisticToProcess.SetInformations(createdRoomsCount, updatedRoomsCount, biggestRoomSize, mostRoomsInApartmentCount);

                await _statisticsRepository.CreateOrUpdateRoomStatistics(statisticToProcess);

            }
            catch (Exception)
            {
                _logger.LogWarning("Something went wrong in RoomsStatisticToProcessMessageConsuemr");
                throw;
            }
        }
    }
}

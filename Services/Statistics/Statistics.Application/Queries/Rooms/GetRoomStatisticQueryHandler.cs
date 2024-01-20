using MediatR;
using Statistics.Application.Dto.Out;
using Statistics.Domain.Entities;
using Statistics.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Application.Queries.Rooms
{
    public class GetRoomStatisticQueryHandler : IRequestHandler<GetRoomStatisticQuery, GetRoomsStatisticResponse>
    {
        private readonly IRoomsStatisticsRepository _roomsStatisticsRepository;

        public GetRoomStatisticQueryHandler(IRoomsStatisticsRepository roomsStatisticsRepository)
        {
            _roomsStatisticsRepository = roomsStatisticsRepository;
        }

        public async Task<GetRoomsStatisticResponse> Handle(GetRoomStatisticQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var roomStatistic = await _roomsStatisticsRepository.GetRoomsAnyStatistics(request.dto.Year, request.dto.Month, request.dto.Day, request.dto.Hour);
                if (roomStatistic == null)
                {
                    throw new FileNotFoundException();
                }
                if (roomStatistic.IsFullyProcessed != true)
                {
                    throw new FileLoadException("Statistic exists but data is not ready yet");
                }
                return new GetRoomsStatisticResponse() { StatisticsEnd = roomStatistic.StatisticsEnd.Value, StatisticsStart = roomStatistic.StatisticsStart.Value, RoomsCreated = roomStatistic.RoomsCreated, RoomStatisticsId = roomStatistic.RoomsStatisticsId, RoomsUpdated = roomStatistic.RoomsUpdated, MostRoomsInApartment = roomStatistic.MostRoomsInApartment, BiggestCreatedRoomSize = roomStatistic.BiggestCreatedRoomSize };

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

using MediatR;
using Microsoft.Extensions.Logging;
using Statistics.Application.Dto.Out;
using Statistics.Domain.Interfaces;

namespace Statistics.Application.Queries.Any.GetAnyStatisticQuery
{
    public class GetAnyStatisticQueryHandler : IRequestHandler<GetAnyStatisticQuery, object>
    {
        private readonly IApartmentsStatisticsRepository _apartmentsStatisticsRepository;
        private readonly IRoomsStatisticsRepository _roomsStatisticsRepository;
        private readonly ITenantsStatisticsRepository _tenantsStatisticsRepository;
        private readonly ILogger<GetAnyStatisticQueryHandler> _logger;

        public GetAnyStatisticQueryHandler(ITenantsStatisticsRepository tenantsStatisticsRepository, IRoomsStatisticsRepository roomsStatisticsRepository, IApartmentsStatisticsRepository apartmentsStatisticsRepository, ILogger<GetAnyStatisticQueryHandler> logger)
        {
            _apartmentsStatisticsRepository = apartmentsStatisticsRepository;
            _roomsStatisticsRepository = roomsStatisticsRepository;
            _tenantsStatisticsRepository = tenantsStatisticsRepository;
            _logger = logger;

        }
        public async Task<object> Handle(GetAnyStatisticQuery request, CancellationToken cancellationToken)
        {
            try
            {

                if (request.dto.Type.Trim().ToLower() == "apartments")
                {
                    var apartmentStatistic = await _apartmentsStatisticsRepository.GetApartmentAnyStatistics(request.dto.Year, request.dto.Month, request.dto.Day, request.dto.Hour);
                    if (apartmentStatistic == null)
                    {
                        throw new FileNotFoundException();
                    }
                    return new GetApartmentsStatisticResponse() { StatisticsEnd = apartmentStatistic.StatisticsEnd.Value, StatisticsStart = apartmentStatistic.StatisticsStart.Value, ApartmentsCreated = apartmentStatistic.ApartmentsCreated, ApartmentsStatisticsId = apartmentStatistic.ApartmentsStatisticsId, ApartmentsUpdated = apartmentStatistic.ApartmentsUpdated, MostApartmentsOwnedByUser = apartmentStatistic.MostApartmentsOwnedByUser };
                }

                else if (request.dto.Type.Trim().ToLower() == "rooms")
                {
                    var roomsStatistic = await _roomsStatisticsRepository.GetRoomsAnyStatistics(request.dto.Year, request.dto.Month, request.dto.Day, request.dto.Hour);
                    if (roomsStatistic == null)
                    {
                        throw new FileNotFoundException();
                    }


                    return new GetRoomsStatisticResponse() { StatisticsEnd = roomsStatistic.StatisticsEnd.Value, StatisticsStart = roomsStatistic.StatisticsStart.Value, RoomsCreated = roomsStatistic.RoomsCreated, RoomStatisticsId = roomsStatistic.RoomsStatisticsId, RoomsUpdated = roomsStatistic.RoomsUpdated, MostRoomsInApartment = roomsStatistic.MostRoomsInApartment };

                }

                else if (request.dto.Type.Trim().ToLower() == "tenants")
                {

                } else
                {
                    throw new FileNotFoundException();
                }





            }
            catch (Exception)
            {

                throw;
            }





        }
    }
}

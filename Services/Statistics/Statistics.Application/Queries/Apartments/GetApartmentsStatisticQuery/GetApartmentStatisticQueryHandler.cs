using MediatR;
using Statistics.Application.Dto.Out;
using Statistics.Domain.Interfaces;

namespace Statistics.Application.Queries.Apartments.GetApartmentsStatisticQuery
{
    public class GetApartmentStatisticQueryHandler : IRequestHandler<GetApartmentStatisticQuery, GetApartmentsStatisticResponse>
    {
        private readonly IApartmentsStatisticsRepository _apartmentsStatisticsRepository;

        public GetApartmentStatisticQueryHandler(IApartmentsStatisticsRepository apartmentsStatisticsRepository)
        {
            _apartmentsStatisticsRepository = apartmentsStatisticsRepository;
        }

        public async Task<GetApartmentsStatisticResponse> Handle(GetApartmentStatisticQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var apartmentStatistic = await _apartmentsStatisticsRepository.GetApartmentAnyStatistics(request.dto.Year, request.dto.Month, request.dto.Day, request.dto.Hour);
                if (apartmentStatistic == null)
                {
                    throw new FileNotFoundException();
                }
                return new GetApartmentsStatisticResponse() { StatisticsEnd = apartmentStatistic.StatisticsEnd.Value, StatisticsStart = apartmentStatistic.StatisticsStart.Value, ApartmentsCreated = apartmentStatistic.ApartmentsCreated, ApartmentsStatisticsId = apartmentStatistic.ApartmentsStatisticsId, ApartmentsUpdated = apartmentStatistic.ApartmentsUpdated, MostApartmentsOwnedByUser = apartmentStatistic.MostApartmentsOwnedByUser };

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

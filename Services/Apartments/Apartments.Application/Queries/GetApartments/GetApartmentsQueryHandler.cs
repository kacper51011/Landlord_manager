using Amazon.Runtime.Internal.Util;
using Apartments.Application.Dtos;
using Apartments.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Apartments.Application.Queries.GetApartments
{
    public class GetApartmentsQueryHandler : IRequestHandler<GetApartmentsQuery, List<ApartmentDto>>
    {
        private readonly IApartmentsRepository _apartmentsRepository;
        private readonly ILogger<GetApartmentsQueryHandler> _logger;

        public GetApartmentsQueryHandler(IApartmentsRepository apartmentsRepository, ILogger<GetApartmentsQueryHandler> logger)
        {
            _apartmentsRepository = apartmentsRepository;
            _logger = logger;
        }

        public async Task<List<ApartmentDto>> Handle(GetApartmentsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<ApartmentDto> list = new List<ApartmentDto>();
                var apartments = await _apartmentsRepository.GetApartmentsByUserId(request.landlordId);
                if (apartments == null)
                {
                    throw new FileNotFoundException("Couldn`t find apartments belonging to this landlord");
                }
                for (var i = 0; i < apartments.Count; i++)
                {
                    ApartmentDto dto = new ApartmentDto() { ApartmentId = apartments[i].ApartmentId, LandlordId = apartments[i].LandlordId, Area = apartments[i].Area, Latitude = apartments[i].Latitude, Longitude = apartments[i].Longitude, Telephone = apartments[i].Telephone };
                    list.Add(dto);
                }
                return list;
            }
            catch (FileNotFoundException ex)
            {
                _logger.LogWarning(404, ex.Message);
                throw ex;
            }

            catch (Exception ex)
            {
                _logger.LogWarning(500, ex.Message);
                throw;
            }
        }
    }
}

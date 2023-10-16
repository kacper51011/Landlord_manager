using Apartments.Application.Dtos;
using Apartments.Application.Queries.GetApartments;
using Apartments.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apartments.Application.Queries.GetApartment
{
    public class GetApartmentQueryHandler : IRequestHandler<GetApartmentQuery, ApartmentDto>
    {
        private readonly IApartmentsRepository _apartmentsRepository;

        public GetApartmentQueryHandler(IApartmentsRepository apartmentsRepository)
        {
            _apartmentsRepository = apartmentsRepository;
        }

        public async Task<ApartmentDto> Handle(GetApartmentQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var apartment = await _apartmentsRepository.GetApartmentByIdAndLandlordId(request.landlordId, request.apartmentId);
                return new ApartmentDto { ApartmentId = apartment.ApartmentId, LandlordId = apartment.LandlordId, Area = apartment.Area, Latitude = apartment.Latitude, Longitude = apartment.Longitude, RoomsNumber = apartment.RoomsNumber, Telephone = apartment.Telephone };

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
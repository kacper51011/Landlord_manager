using Apartments.Application.Dtos;
using Apartments.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apartments.Application.Queries.GetApartments
{
    public class GetApartmentsQueryHandler: IRequestHandler<GetApartmentsQuery, List<ApartmentDto>>
    {
        private readonly IApartmentsRepository _apartmentsRepository;

        public GetApartmentsQueryHandler(IApartmentsRepository apartmentsRepository)
        {
            _apartmentsRepository = apartmentsRepository;
        }

        public async Task<List<ApartmentDto>> Handle(GetApartmentsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<ApartmentDto> list = new List<ApartmentDto>();
                var apartments = await _apartmentsRepository.GetApartmentsByUserId(request.landlordId);

                for (var i = 0; i < apartments.Count; i++)
                {
                    ApartmentDto dto = new ApartmentDto() { ApartmentId = apartments[i].ApartmentId, LandlordId = apartments[i].LandlordId, Area= apartments[i].Area, Latitude = apartments[i].Latitude, Longitude = apartments[i].Longitude, Telephone = apartments[i].Telephone };
                    list.Add(dto);
                }
                return list;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

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
            throw new NotImplementedException();
        }
    }
}
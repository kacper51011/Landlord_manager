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
        private readonly IApartmentsRepository _repository;

        public GetApartmentsQueryHandler(IApartmentsRepository repository)
        {
            _repository = repository;
        }

        public Task<List<ApartmentDto>> Handle(GetApartmentsQuery request, CancellationToken cancellationToken)
        {
throw new NotImplementedException();
        }
    }
}

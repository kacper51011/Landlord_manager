
using Apartments.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apartments.Application.Queries.GetApartments
{
    public record GetApartmentsQueryRequest() : IRequest<List<ApartmentDto>>
    {
    }
}

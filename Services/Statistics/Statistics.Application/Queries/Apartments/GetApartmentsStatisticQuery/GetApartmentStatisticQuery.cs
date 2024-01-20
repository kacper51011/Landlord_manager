using MediatR;
using Statistics.Application.Dto.In;
using Statistics.Application.Dto.Out;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Application.Queries.Apartments.GetApartmentsStatisticQuery
{
    public record GetApartmentStatisticQuery(GetStatisticDto dto): IRequest<GetApartmentsStatisticResponse>
    {
    }
}

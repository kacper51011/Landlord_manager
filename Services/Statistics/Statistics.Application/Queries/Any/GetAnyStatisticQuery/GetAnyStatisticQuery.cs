using MediatR;
using Statistics.Application.Dto.In;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Application.Queries.Any.GetAnyStatisticQuery
{
    public record GetAnyStatisticQuery(GetAnyStatisticDto dto): IRequest<object>
    {
    }
}

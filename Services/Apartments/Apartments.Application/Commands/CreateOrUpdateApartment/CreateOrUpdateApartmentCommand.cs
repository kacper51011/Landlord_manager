using Apartments.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apartments.Application.Commands.CreateOrUpdateApartment
{
    public record CreateOrUpdateApartmentCommand(ApartmentDto dto) : IRequest
    {
    }
}

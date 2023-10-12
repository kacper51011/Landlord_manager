using Amazon.Runtime.Internal;
using Apartments.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apartments.Application.Commands.CreateOrUpdateApartment
{
    public record CreateOrUpdateCommand(string userId, ApartmentDto dto) : IRequest
    {
    }
}

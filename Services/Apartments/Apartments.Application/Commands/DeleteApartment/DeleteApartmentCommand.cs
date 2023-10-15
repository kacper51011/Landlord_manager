using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apartments.Application.Commands.DeleteApartment
{
    public record DeleteApartmentCommand(string userId, string apartmentId) : IRequest<bool>
    {
    }
}

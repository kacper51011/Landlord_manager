using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rooms.Application.Commands.DeleteAllRooms
{
    public record DeleteAllRoomsCommand(string landlordId, string apartmentId): IRequest
    {
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rooms.Application.Commands.DeleteRoom
{
    public record DeleteRoomCommand(string landlordId, string apartmentId, string roomId) : IRequest
    {
    }
}

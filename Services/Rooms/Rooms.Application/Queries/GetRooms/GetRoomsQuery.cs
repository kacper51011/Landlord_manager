using MediatR;
using Rooms.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rooms.Application.Queries.GetRooms
{
    public record GetRoomsQuery(string apartmentId) : IRequest<List<RoomDto>>
    {
    }
}

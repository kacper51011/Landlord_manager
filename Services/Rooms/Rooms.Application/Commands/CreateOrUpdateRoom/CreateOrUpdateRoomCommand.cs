using MediatR;
using Rooms.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rooms.Application.Commands.CreateOrUpdateRoom
{
    public record CreateOrUpdateRoomCommand(RoomDto RoomDto): IRequest
    {
    }
}

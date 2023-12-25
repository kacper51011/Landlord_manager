using Contracts;
using Contracts.RoomsServiceEvents;
using MassTransit;
using MediatR;
using Rooms.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rooms.Application.Commands.DeleteRoom
{
    public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand>
    {
        private readonly IRoomsRepository _roomRepository;
        public DeleteRoomCommandHandler(IRoomsRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }
        public async Task Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            var room = await _roomRepository.GetRoomByIdAndLandlordId(request.landlordId, request.roomId);
            if (room == null)
            {
                return;
            }
            else
            {
                await _roomRepository.DeleteRoom(request.roomId);
            }

        }
    }
}

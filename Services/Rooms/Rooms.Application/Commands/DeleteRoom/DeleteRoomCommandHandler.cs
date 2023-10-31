using MassTransit;
using MediatR;
using Rooms.Application.Contracts;
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
        private readonly IPublishEndpoint _publishEndpoint;
        public DeleteRoomCommandHandler(IRoomsRepository roomRepository, IPublishEndpoint publishEndpoint)
        {
            _roomRepository = roomRepository;
            _publishEndpoint = publishEndpoint;
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
                await _publishEndpoint.Publish(new RoomDeletedMessage { apartmentId = request.apartmentId });
            }

        }
    }
}

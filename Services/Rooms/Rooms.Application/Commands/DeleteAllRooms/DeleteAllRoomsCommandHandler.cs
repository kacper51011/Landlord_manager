using MediatR;
using Rooms.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rooms.Application.Commands.DeleteAllRooms
{
    public class DeleteAllRoomsCommandHandler : IRequestHandler<DeleteAllRoomsCommand>
    {
        private readonly IRoomsRepository _roomsRepository;

        public DeleteAllRoomsCommandHandler(IRoomsRepository roomsRepository)
        {
            _roomsRepository = roomsRepository;
        }
        public async Task Handle(DeleteAllRoomsCommand request, CancellationToken cancellationToken)
        {
            //await _roomsRepository.DeleteAllRoomsInApartment(request.landlordId, request.apartmentId);
        }
    }
}

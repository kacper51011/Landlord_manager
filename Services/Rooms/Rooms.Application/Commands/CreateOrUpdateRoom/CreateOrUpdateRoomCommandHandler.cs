using Contracts;
using Contracts.RoomsServiceEvents;
using MassTransit;
using MediatR;
using Rooms.Application.Dto;
using Rooms.Domain.Entities;
using Rooms.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rooms.Application.Commands.CreateOrUpdateRoom
{
    public class CreateOrUpdateRoomCommandHandler : IRequestHandler<CreateOrUpdateRoomCommand>
    {
        private readonly IRoomsRepository _roomsRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        public CreateOrUpdateRoomCommandHandler(IRoomsRepository roomsRepository, IBus publishEndpoint)
        {
            _roomsRepository = roomsRepository;
            _publishEndpoint = publishEndpoint;
        }
        public async Task Handle(CreateOrUpdateRoomCommand request, CancellationToken cancellationToken)
        {
            try
            {
                RoomDto x = request.RoomDto;
                if (request.RoomDto.RoomId == null)
                {
                    Room room = Room.CreateRoom(x.ApartmentId, x.LandlordId, x.Name, x.Surface, x.AnglesCoordinates, x.MaxTenantsNumber, x.CurrentTenantsNumber, x.MonthlyRent);
                    await _roomsRepository.CreateOrUpdateRoom(room);
                    return;
                } else
                {
                    var room = await _roomsRepository.GetRoomById(request.RoomDto.RoomId);
                    if (room == null)
                    {
                        room = Room.CreateRoom(x.ApartmentId, x.LandlordId, x.Name, x.Surface, x.AnglesCoordinates, x.MaxTenantsNumber, x.CurrentTenantsNumber, x.MonthlyRent);
                        await _roomsRepository.CreateOrUpdateRoom(room);

                    } else
                    {
                        room.UpdateRoom(x.Name, x.Surface, x.AnglesCoordinates, x.MaxTenantsNumber, x.CurrentTenantsNumber, x.MonthlyRent);
                    }
                    
                }
                return;


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

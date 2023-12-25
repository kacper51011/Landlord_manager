using MediatR;
using Rooms.Application.Dto;
using Rooms.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rooms.Application.Queries.GetRooms
{
    public class GetRoomsQueryHandler : IRequestHandler<GetRoomsQuery, List<RoomDto>>
    {
        private readonly IRoomsRepository _roomsRepository;
        public GetRoomsQueryHandler(IRoomsRepository roomsRepository)
        {
            _roomsRepository = roomsRepository;
        }
        public async Task<List<RoomDto>> Handle(GetRoomsQuery request, CancellationToken cancellationToken)
        {
            try
            {
            var response = await _roomsRepository.GetRoomsByApartmentId(request.apartmentId);
            
            List<RoomDto> result = new List<RoomDto>();

            for (int i = 0; i < response.Count; i++)
            {
                var curr = response[i];
                var roomDto = new RoomDto() {RoomId = curr.RoomId, LandlordId= curr.LandlordId, ApartmentId= curr.ApartmentId, Name= curr.Name, AnglesCoordinates=curr.AnglesCoordinates, Surface=curr.Surface };
                result.Add(roomDto);
            }
            return result;
            }
            catch (Exception)
            {

                throw;
            }

            
            
        }
    }
}

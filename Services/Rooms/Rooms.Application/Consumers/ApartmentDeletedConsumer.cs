using Contracts.ApartmentsServiceEvents;
using MassTransit;
using Rooms.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rooms.Application.Consumers
{
    public class ApartmentDeletedConsumer : IConsumer<ApartmentDeletedEvent>
    {
        private readonly IRoomsRepository _roomsRepository;

        public ApartmentDeletedConsumer(IRoomsRepository roomsRepository)
        {
            _roomsRepository = roomsRepository;
        }
        public async Task Consume(ConsumeContext<ApartmentDeletedEvent> context)
        {
            try
            {
                var rooms = await _roomsRepository.GetRoomsByApartmentId(context.Message.ApartmentId);
                if (rooms == null)
                {
                    return;
                }
                foreach (var room in rooms)
                {
                   await _roomsRepository.DeleteRoom(room.RoomId);
                }
                
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}

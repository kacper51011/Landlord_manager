using Contracts.TenantsServiceEvents;
using MassTransit;
using Rooms.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rooms.Application.Consumers
{
    public class TenantDeletedConsumer : IConsumer<TenantDeletedEvent>
    {
        private readonly IRoomsRepository _roomsRepository;

        public TenantDeletedConsumer(IRoomsRepository roomsRepository)
        {
            _roomsRepository = roomsRepository;
        }
        public async Task Consume(ConsumeContext<TenantDeletedEvent> context)
        {
            try
            {
                var room = await _roomsRepository.GetRoomById(context.Message.RoomId);
                if (room == null)
                {
                    return;
                }
                room.DecrementTenantNumber();
                await _roomsRepository.CreateOrUpdateRoom(room);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

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
    public class TenantAddedConsumer : IConsumer<TenantAddedEvent>
    {
        private readonly IRoomsRepository _roomsRepository;

        public TenantAddedConsumer(IRoomsRepository roomsRepository)
        {
            _roomsRepository = roomsRepository;
            
        }

        public async Task Consume(ConsumeContext<TenantAddedEvent> context)
        {
            try
            {
                var room = await _roomsRepository.GetRoomById(context.Message.roomId);
                if (room == null)
                {
                    return;
                }
                room.IncrementTenantNumber();
                await _roomsRepository.CreateOrUpdateRoom(room);
            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}

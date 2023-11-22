using Contracts.ApartmentsServiceEvents;
using MassTransit;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<ApartmentDeletedConsumer> _logger; 

        public ApartmentDeletedConsumer(IRoomsRepository roomsRepository, ILogger<ApartmentDeletedConsumer> logger)
        {
            _roomsRepository = roomsRepository;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<ApartmentDeletedEvent> context)
        {
            try
            {
                var room = await _roomsRepository.GetRoomById(context.Message.RoomId);
                if (room == null)
                {
                    _logger.LogInformation($"Couldn`t find room with Id {context.Message.RoomId}");
                    return;

                }
                await _roomsRepository.DeleteRoom(room.RoomId);
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in ApartmentDeletedConsumer when processing room with Id{context.Message.RoomId}");
                throw ex;
            }

        }
    }
}

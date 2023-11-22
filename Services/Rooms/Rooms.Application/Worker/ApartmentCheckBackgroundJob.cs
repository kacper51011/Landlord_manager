using Contracts.RoomsServiceEvents;
using MassTransit;
using Microsoft.Extensions.Logging;
using Quartz;
using Rooms.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rooms.Application.Worker
{
    public class ApartmentCheckBackgroundJob : IJob
    {
        private readonly IRoomsRepository _roomsRepository;
        private readonly ILogger<ApartmentCheckBackgroundJob> _logger;
        private readonly IPublishEndpoint _publishEndpoint;
        public ApartmentCheckBackgroundJob(IRoomsRepository roomsRepository,  ILogger<ApartmentCheckBackgroundJob> logger, IPublishEndpoint publishEndpoint)
        {
            _roomsRepository = roomsRepository;
            _logger = logger;
            _publishEndpoint = publishEndpoint;
            
        }
        public async Task Execute(IJobExecutionContext context)
        {
            var rooms = await _roomsRepository.GetOldestCheckedRooms();
            if (rooms == null || rooms.Count == 0)
            {
                _logger.LogInformation($"No rooms");
                return;
            }
            foreach (var room in rooms)
            {
                room.SetLastCheckedDate();
                await _publishEndpoint.Publish(new RoomCheckedEvent {ApartmentId = room.ApartmentId });
                await _roomsRepository.CreateOrUpdateRoom(room);
                _logger.LogInformation($"room with Id {room.RoomId} sent to check");

            }
        }
    }
}

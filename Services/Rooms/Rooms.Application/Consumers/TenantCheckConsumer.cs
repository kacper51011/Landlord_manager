using Contracts.RoomsServiceEvents;
using Contracts.TenantsServiceEvents;
using MassTransit;
using Microsoft.Extensions.Logging;
using Rooms.Domain.Entities;
using Rooms.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rooms.Application.Consumers
{
    public class TenantCheckConsumer : IConsumer<TenantCheckedEvent>
    {
        IRoomsRepository _roomsRepository;
        ILogger<TenantCheckConsumer> _logger;
        IPublishEndpoint _publishEndpoint;


        public TenantCheckConsumer(IRoomsRepository roomsRepository, ILogger<TenantCheckConsumer> logger, IPublishEndpoint endpoint)
        {
            _roomsRepository = roomsRepository;
            _logger = logger;
            _publishEndpoint = endpoint;

        }
        public async Task Consume(ConsumeContext<TenantCheckedEvent> context)
        {
            try
            {
                var room = await _roomsRepository.GetRoomById(context.Message.RoomId);
                if (room != null)
                {
                    _logger.LogInformation($"pokój lokatora o id {context.Message.TenantId} wciąż istnieje");
                    return;
                }
                if (room == null)
                {
                    _logger.LogInformation($"pokój lokatora o id został usunięty, wysłanie na kolejkę prośbę o usunięcie lokatora");
                    await _publishEndpoint.Publish(new RoomDeletedEvent { TenantId = context.Message.TenantId });

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}

using Apartments.Domain.Interfaces;
using Contracts.ApartmentsServiceEvents;
using Contracts.RoomsServiceEvents;
using MassTransit;
using MassTransit.Transports;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apartments.Application.Consumers
{
    public class RoomCheckedConsumer : IConsumer<RoomCheckedMessage>
    {
        private readonly IApartmentsRepository _apartmentsRepository;
        private readonly ILogger<RoomCheckedConsumer> _logger;
        private readonly IPublishEndpoint _publishEndpoint;
        public RoomCheckedConsumer(IApartmentsRepository apartmentsRepository, ILogger<RoomCheckedConsumer> logger, IPublishEndpoint publishEndpoint)
        {
            _apartmentsRepository = apartmentsRepository;
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }


        public async Task Consume(ConsumeContext<RoomCheckedMessage> context)
        {
            var apartment = await _apartmentsRepository.GetApartmentById(context.Message.ApartmentId);
            if (apartment != null)
            {
                _logger.LogInformation($"Apartment of room with Id {context.Message.RoomId}");
                return;
            }

            await _publishEndpoint.Publish(new ApartmentDeletedMessage { RoomId = context.Message.RoomId });
            _logger.LogInformation($"Apartment of room with id {context.Message.RoomId} was deleted, sending message on a queue");

        }
    }
}

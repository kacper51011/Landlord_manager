using Contracts.RoomsServiceEvents;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenants.Application.Commands.DeleteAllTenantsInRoom;

namespace Tenants.Application.Consumers
{
    public class RoomDeletedConsumer : IConsumer<RoomDeletedEvent>
    {
		private readonly IMediator _mediator;
        private readonly ILogger<RoomDeletedConsumer> _logger;
        public RoomDeletedConsumer(IMediator mediator, ILogger<RoomDeletedConsumer> logger)
        {
            _mediator = mediator;
            _logger = logger;

        }
        public async Task Consume(ConsumeContext<RoomDeletedEvent> context)
        {
			try
			{
                var command = new DeleteAllTenantsInRoomCommand(context.Message.RoomId);
                await _mediator.Send(command);
			}
			catch (Exception)
			{

				throw;
			}
        }
    }
}

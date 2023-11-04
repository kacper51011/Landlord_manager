using Contracts.RoomsServiceEvents;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenants.Application.Consumers
{
    public class RoomDeletedConsumer : IConsumer<RoomDeletedEvent>
    {
        public async Task Consume(ConsumeContext<RoomDeletedEvent> context)
        {
            throw new NotImplementedException();
        }
    }
}

using Contracts.TenantsServiceEvents;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rooms.Application.Consumers
{
    public class TenantAddedConsumer : IConsumer<TenantAddedEvent>
    {

        public async Task Consume(ConsumeContext<TenantAddedEvent> context)
        {
            throw new NotImplementedException();
        }
    }
}

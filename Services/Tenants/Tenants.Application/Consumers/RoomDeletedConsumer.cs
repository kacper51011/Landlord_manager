using Contracts.RoomsServiceEvents;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenants.Domain.Interfaces;

namespace Tenants.Application.Consumers
{
    public class RoomDeletedConsumer : IConsumer<RoomDeletedMessage>
    {
        private readonly ITenantsRepository _tenantsRepository;
        private readonly ILogger<RoomDeletedConsumer> _logger;

        public RoomDeletedConsumer(ITenantsRepository tenantsRepository, ILogger<RoomDeletedConsumer> logger)
        {
            _tenantsRepository = tenantsRepository;
            _logger = logger;
            
        }
        public async Task Consume(ConsumeContext<RoomDeletedMessage> context)
        {
            try
            {

                var tenant = await _tenantsRepository.GetTenantById(context.Message.TenantId);
                if (tenant == null)
                {
                    return;
                }
                await _tenantsRepository.DeleteTenant(tenant.TenantId);
                _logger.LogInformation($"Deleted tenant with Id {tenant.TenantId}");
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
    }
}

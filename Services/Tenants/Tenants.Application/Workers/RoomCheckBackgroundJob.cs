using Contracts.TenantsServiceEvents;
using MassTransit;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenants.Domain.Entities;
using Tenants.Domain.Interfaces;

namespace Tenants.Application.Workers
{
    [DisallowConcurrentExecution]
    public class RoomCheckBackgroundJob : IJob
    {
        private readonly ITenantsRepository _tenantsRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<RoomCheckBackgroundJob> _logger;
        public RoomCheckBackgroundJob(ITenantsRepository tenantsRepository, IPublishEndpoint publishEndpoint, ILogger<RoomCheckBackgroundJob> logger)
        {
            _tenantsRepository = tenantsRepository;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            var tenants = await _tenantsRepository.GetOldestCheckedTenants();
            if(tenants == null || tenants.Count == 0)
            {
                _logger.LogInformation($"No tenants");
                return;
            }
            foreach (var tenant in tenants)
            {
                tenant.SetLastCheckedDate();
                await _publishEndpoint.Publish(new TenantCheckedMessage { RoomId = tenant.RoomId, TenantId = tenant.TenantId});
                await _tenantsRepository.CreateOrUpdateTenant(tenant);
                _logger.LogInformation($"tenant with Id {tenant.TenantId} sent to check");

            }

        }
    }
}

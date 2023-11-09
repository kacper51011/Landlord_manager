using Contracts.TenantsServiceEvents;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenants.Domain.Interfaces;

namespace Tenants.Application.Commands.DeleteTenant
{
    public class DeleteTenantCommandHandler : IRequestHandler<DeleteTenantCommand, bool>
    {
        private readonly ITenantsRepository _tenantsRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        public DeleteTenantCommandHandler(ITenantsRepository tenantsRepository, IPublishEndpoint publishEndpoint)
        {
            _tenantsRepository = tenantsRepository;
            _publishEndpoint = publishEndpoint;
        }
        public async Task<bool> Handle(DeleteTenantCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var tenant = await _tenantsRepository.GetTenantById(request.tenantId);
                if (tenant == null)
                {
                    return false;
                }
                await _tenantsRepository.DeleteTenant(tenant.TenantId);
                await _publishEndpoint.Publish(new TenantDeletedEvent { RoomId = request.roomId });
                return true;
                
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}

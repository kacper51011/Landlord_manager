using Contracts.TenantsServiceEvents;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenants.Domain.Interfaces;

namespace Tenants.Application.Commands.DeleteTenant
{
    public class DeleteTenantCommandHandler : IRequestHandler<DeleteTenantCommand>
    {
        private readonly ITenantsRepository _tenantsRepository;
        private readonly ILogger<DeleteTenantCommandHandler> _logger;
        public DeleteTenantCommandHandler(ITenantsRepository tenantsRepository, ILogger<DeleteTenantCommandHandler> logger)
        {
            _tenantsRepository = tenantsRepository;
            _logger = logger;
        }
        public async Task Handle(DeleteTenantCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var tenant = await _tenantsRepository.GetTenantById(request.tenantId);
                if (tenant == null)
                {
                    throw new FileNotFoundException($"Couldn`t delete tenant with Id {request.tenantId}");
                }
                await _tenantsRepository.DeleteTenant(tenant.TenantId);
                
            }
            catch (FileNotFoundException ex)
            {

                _logger.LogWarning(404, ex, ex.Message);
                throw ex;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

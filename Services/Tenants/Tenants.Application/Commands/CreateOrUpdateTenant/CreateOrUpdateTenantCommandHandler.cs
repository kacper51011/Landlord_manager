using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Tenants.Application.Dtos;
using Tenants.Domain.Entities;
using Tenants.Domain.Interfaces;

namespace Tenants.Application.Commands.CreateOrUpdateTenant
{
    public class CreateOrUpdateTenantCommandHandler : IRequestHandler<CreateOrUpdateTenantCommand>
    {
        private readonly ITenantsRepository _tenantsRepository;
        private readonly ILogger<CreateOrUpdateTenantCommandHandler> _logger;

        public CreateOrUpdateTenantCommandHandler(ITenantsRepository tenantsRepository, ILogger<CreateOrUpdateTenantCommandHandler> logger)
        {
            
            _tenantsRepository = tenantsRepository;
            _logger = logger;
        }
        public async Task Handle(CreateOrUpdateTenantCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var x = request.tenantDto;
                if(x.TenantId == null)
                {
                    var createdTenant = Tenant.Create(x.RoomId, x.FirstName, x.LastName, x.Age, x.IsStudying, x.IsWorking, x.Email, x.Rent, x.ContractStart, x.ContractEnd, x.Telephone);
                    await _tenantsRepository.CreateOrUpdateTenant(createdTenant);
                    return;

                }

                var tenant = await _tenantsRepository.GetTenantById(request.tenantDto.TenantId);
                var tenantToSave = Tenant.Create(x.RoomId, x.FirstName, x.LastName, x.Age, x.IsStudying, x.IsWorking, x.Email, x.Rent, x.ContractStart, x.ContractEnd, x.Telephone);
                if (tenant == null)
                {
                    await _tenantsRepository.CreateOrUpdateTenant(tenantToSave);
                    return;
                } else
                {
                    tenant.Update(tenantToSave);
                    await _tenantsRepository.CreateOrUpdateTenant(tenant);
                    return;

                }


            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}

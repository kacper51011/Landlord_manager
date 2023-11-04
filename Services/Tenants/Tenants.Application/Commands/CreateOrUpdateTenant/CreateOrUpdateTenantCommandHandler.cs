using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenants.Application.Dtos;

namespace Tenants.Application.Commands.CreateOrUpdateTenant
{
    public class CreateOrUpdateTenantCommandHandler : IRequestHandler<CreateOrUpdateTenantCommand, TenantDto>
    {
        public async Task<TenantDto> Handle(CreateOrUpdateTenantCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

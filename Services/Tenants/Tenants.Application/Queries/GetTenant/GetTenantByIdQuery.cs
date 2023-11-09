using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenants.Application.Dtos;

namespace Tenants.Application.Queries.GetTenant
{
    public record GetTenantByIdQuery(string tenantId): IRequest<TenantDto>
    {
    }
}

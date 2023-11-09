
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenants.Application.Commands.DeleteTenant
{
    public record DeleteTenantCommand(string roomId, string tenantId): IRequest<bool>
    {
    }
}

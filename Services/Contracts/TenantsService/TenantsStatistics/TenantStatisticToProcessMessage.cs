using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.TenantsService.TenantsStatistics
{
    public record TenantStatisticToProcessMessage
    {
        public string TenantStatisticId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.TenantsServiceEvents
{
    public record TenantCheckedEvent
    {
        public string TenantId {  get; set; }
        public string RoomId { get; set; }
    }
}

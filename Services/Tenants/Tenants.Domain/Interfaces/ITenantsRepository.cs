using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenants.Domain.Entities;

namespace Tenants.Domain.Interfaces
{
    public interface ITenantsRepository
    {
        public Task<List<Tenant>> GetTenantsByRoomId(string roomId);
        public Task CreateOrUpdateTenant(Tenant tenant);
        public Task<Tenant> GetTenantByIdAndRoomId(string tenantId, string roomId);
        public Task DeleteTenant(string tenantId);

        public Task DeleteAllTenantsInRoom(string RoomId);
        public Task<Tenant> GetTenantById(string tenantId);
    }
}

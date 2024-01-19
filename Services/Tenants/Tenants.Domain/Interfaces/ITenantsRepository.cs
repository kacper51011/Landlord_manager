using MongoDB.Bson.IO;
using Tenants.Domain.Entities;

namespace Tenants.Domain.Interfaces
{
    public interface ITenantsRepository
    {
        public Task<List<Tenant>> GetTenantsByRoomId(string roomId);
        public Task<List<Tenant>> GetOldestCheckedTenants();
        public Task CreateOrUpdateTenant(Tenant tenant);
        public Task<Tenant> GetTenantByIdAndRoomId(string tenantId, string roomId);
        public Task DeleteTenant(string tenantId);
        public Task DeleteAllTenantsInRoom(string RoomId);
        public Task<Tenant> GetTenantById(string tenantId);

        // statistics gets
        public Task<int> GetMostTenantsInOneRoomCount(DateTime endDate);
        public Task<int> GetUpdatedTenantsCount(DateTime startDate, DateTime endDate);
        public Task<int> GetCreatedTenantsCount(DateTime startDate, DateTime endDate);
        public Task<int> GetHighestRentValue(DateTime startDate, DateTime endDate);
    }
}

using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenants.Domain.Entities;
using Tenants.Domain.Interfaces;
using Tenants.Infrastructure.Settings;

namespace Tenants.Infrastructure.Repositories
{
    public class TenantsRepository : ITenantsRepository
    {
        private IMongoCollection<Tenant> _tenantCollection;
        public TenantsRepository(IOptions<MongoSettings> tenantsDatabaseSettings)
        {
            
        }
        public Task CreateOrUpdateTenant(Tenant tenant)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAllTenantsInRoom(string RoomId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTenant(string tenantId)
        {
            throw new NotImplementedException();
        }

        public Task<Tenant> GetTenantById(string tenantId)
        {
            throw new NotImplementedException();
        }

        public Task<Tenant> GetTenantByIdAndRoomId(string tenantId, string roomId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Tenant>> GetTenantsByRoomId(string roomId)
        {
            throw new NotImplementedException();
        }
    }
}

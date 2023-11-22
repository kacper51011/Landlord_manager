using Microsoft.Extensions.Options;
using MongoDB.Bson;
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
            var mongoClient = new MongoClient(tenantsDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(tenantsDatabaseSettings.Value.DatabaseName);

            _tenantCollection = mongoDatabase.GetCollection<Tenant>(tenantsDatabaseSettings.Value.CollectionName);
        }
        public async Task CreateOrUpdateTenant(Tenant tenant)
        {
            await _tenantCollection.ReplaceOneAsync(x => x.TenantId == tenant.TenantId, tenant, new ReplaceOptions()
            {
                IsUpsert = true
            });
        }

        

        public Task DeleteAllTenantsInRoom(string roomId)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteTenant(string tenantId)
        {
            await _tenantCollection.FindOneAndDeleteAsync(x => x.TenantId == tenantId);
        }

        public async Task<Tenant> GetTenantById(string tenantId)
        {
            return await _tenantCollection.FindAsync(x => x.TenantId == tenantId).Result.FirstAsync();
        }

        public async Task<List<Tenant>> GetOldestCheckedTenants()
        {

            var builder = Builders<Tenant>.Filter;

            var filter = builder.Lte(x => x.LastCheckedDate, DateTime.UtcNow);

            var options = new FindOptions<Tenant>()
            {
                Sort = Builders<Tenant>.Sort.Descending(x => x.LastCheckedDate),
                BatchSize = 5,
            };

            return await _tenantCollection.FindAsync(filter, options).Result.ToListAsync();
        }

        public async Task<Tenant> GetTenantByIdAndRoomId(string tenantId, string roomId)
        {
            var builder = Builders<Tenant>.Filter;
            var filter = builder.Eq(a => a.RoomId, roomId) & builder.Eq(a => a.TenantId, tenantId);

            return await _tenantCollection.FindAsync(filter).Result.FirstAsync();
        }

        public async Task<List<Tenant>> GetTenantsByRoomId(string roomId)
        {
            return await _tenantCollection.FindAsync(x => x.RoomId == roomId).Result.ToListAsync();
        }
    }
}

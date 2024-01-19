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
            return await _tenantCollection.Find(x => x.TenantId == tenantId).FirstOrDefaultAsync();
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

            return await _tenantCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<Tenant>> GetTenantsByRoomId(string roomId)
        {
            return await _tenantCollection.Find(x => x.RoomId == roomId).ToListAsync();
        }

        public async Task<int> GetMostTenantsInOneRoomCount(DateTime endDate)
        {
            var filter = Builders<Tenant>.Filter.Lte(a => a.CreationDate, endDate);
            var aggregation = _tenantCollection.Aggregate()
            .Match(filter)
            .Group(
                key => key.RoomId,
                group => new
                {
                    RoomId = group.Key,
                    objectsCount = group.Count()
                }
                )
            .SortBy(x => x.objectsCount);
            var result = await aggregation.ToListAsync();
            return result[0].objectsCount;
        }
        public async Task<int> GetUpdatedTenantsCount(DateTime startDate, DateTime endDate)
        {
            var builder = Builders<Tenant>.Filter;
            var dateRangeFilter = builder.Gte(a => a.UpdateDates.Min(), startDate) & builder.Lte(a => a.UpdateDates.Max(), endDate);
            var versionFilter = builder.Gt(a => a.Version, 1);
            var combinedFilter = versionFilter & dateRangeFilter;

            var result = await _tenantCollection.CountDocumentsAsync(combinedFilter);

            return (int)result;
        }
        public async Task<int> GetCreatedTenantsCount(DateTime startDate, DateTime endDate)
        {
            //filter for number of created tenant from start - end date
            var builder = Builders<Tenant>.Filter;
            var filter = builder.Gte(a => a.CreationDate, startDate) & builder.Lt(a => a.CreationDate, endDate);

            var result = await _tenantCollection.CountDocumentsAsync(filter);


            return (int)result;

        }

        public async Task<int> GetHighestRentValue(DateTime startDate, DateTime endDate)
        {
            var builder = Builders<Tenant>.Filter;
            var filter = builder.Gte(a => a.CreationDate, startDate) & builder.Lt(a => a.CreationDate, endDate);
            var sort = Builders<Tenant>.Sort.Descending(x => x.Rent);
            var result = await _tenantCollection.Find(filter).Sort(sort).FirstOrDefaultAsync();

            return result.Rent;
        }
    }
}

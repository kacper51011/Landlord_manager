using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenants.Domain;
using Tenants.Domain.Interfaces;
using Tenants.Infrastructure.Settings;

namespace Tenants.Infrastructure.Repositories
{
    public class TenantsStatisticsRepository: ITenantsStatisticsRepository
    {
        private IMongoCollection<TenantsStatistics> _tenantsStatisticsCollection;
        public TenantsStatisticsRepository(IOptions<MongoSettings> tenantsDatabaseSettings)
        {
            var mongoClient = new MongoClient(
    tenantsDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                tenantsDatabaseSettings.Value.DatabaseName);

            _tenantsStatisticsCollection = mongoDatabase.GetCollection<TenantsStatistics>(
                tenantsDatabaseSettings.Value.CollectionStatisticsName);

        }
        public async Task CreateOrUpdateTenantsStatistics(TenantsStatistics tenantsStatistics)
        {
            await _tenantsStatisticsCollection.ReplaceOneAsync(x => x.TenantsStatisticsId == tenantsStatistics.TenantsStatisticsId, tenantsStatistics, new ReplaceOptions()
            {
                IsUpsert = true
            });
        }

        public async Task<TenantsStatistics> GetTenantsStatisticsById(string tenantStatisticsId)
        {
            return await _tenantsStatisticsCollection.Find(x => x.TenantsStatisticsId == tenantStatisticsId).FirstOrDefaultAsync();

        }
        public async Task<TenantsStatistics> GetUnproccessedTenantsStatistics()
        {
            var builder = Builders<TenantsStatistics>.Filter;
            var filter = builder.Eq(x => x.AreInformationsSubmitted, false);
            var sort = Builders<TenantsStatistics>.Sort.Ascending(x => x.LastModifiedDate);

            var returnValue = await _tenantsStatisticsCollection.Find(filter).Sort(sort).FirstOrDefaultAsync();
            return returnValue;
        }
        public async Task<TenantsStatistics> GetNotSendTenantsStatistics()
        {
            var builder = Builders<TenantsStatistics>.Filter;
            var filter = builder.Eq(x => x.IsSendToStatisticsService, false) & builder.Eq(x => x.AreInformationsSubmitted, true);

            var update = Builders<TenantsStatistics>.Update.Set(x => x.IsSendToStatisticsService, true);

            var sort = Builders<TenantsStatistics>.Sort.Descending(x => x.LastModifiedDate);
            var returnValue = await _tenantsStatisticsCollection.FindOneAndUpdateAsync(filter, update);
            //var returnValue2 = await _tenantsStatisticsCollection.Fin(filter, )
            //var returnValue = await _tenantsStatisticsCollection.FindOneAndUpdate

            return returnValue;
        }

        public async Task<TenantsStatistics> GetTenantsAnyStatistics(int year, int? month, int? day, int? hour)
        {
            var builder = Builders<TenantsStatistics>.Filter;
            var anyFilter = builder.Eq(a => a.Year.Value, year) & builder.Eq(a => a.Month.Value, month) & builder.Eq(a => a.Day.Value, day) & builder.Eq(a => a.Hour.Value, hour);
            var combinedFilter = anyFilter;

            var result = await _tenantsStatisticsCollection.Find(combinedFilter).FirstOrDefaultAsync();

            return result;
        }
    }
}

using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Statistics.Domain.Entities;
using Statistics.Domain.Interfaces;
using Statistics.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Infrastructure.Repositories
{
    public class TenantsStatisticsRepository : ITenantsStatisticsRepository
    {
        private IMongoCollection<TenantsStatistics> _tenantsStatisticsCollection;
        public TenantsStatisticsRepository(IOptions<MongoSettings> databaseSettings)

        {
            var mongoClient = new MongoClient(
                databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                databaseSettings.Value.DatabaseName);

            _tenantsStatisticsCollection = mongoDatabase.GetCollection<TenantsStatistics>(
                databaseSettings.Value.TenantsCollectionName);
        }
        public async Task CreateOrUpdateTenantsStatistics(TenantsStatistics tenantsStatistics)
        {
            var result = await _tenantsStatisticsCollection.ReplaceOneAsync(x => x.TenantsStatisticsId == tenantsStatistics.TenantsStatisticsId, tenantsStatistics, new ReplaceOptions()
            {
                IsUpsert = true
            });

        }

        public async Task<TenantsStatistics> GetTenantsStatisticsById(string apartmentStatisticsId)
        {
            return await _tenantsStatisticsCollection.FindAsync(x => x.TenantsStatisticsId == apartmentStatisticsId).Result.FirstAsync();

        }
        public async Task<TenantsStatistics> GetTenantsYearStatistics(int year)
        {

            var builder = Builders<TenantsStatistics>.Filter;
            var yearFilter = builder.Eq(a => a.Year.Value, year);
            var nullFilter = builder.Eq(a => a.Month, null) & builder.Eq(a => a.Day, null) & builder.Eq(a => a.Hour, null);
            var combinedFilter = yearFilter & nullFilter;

            var result = await _tenantsStatisticsCollection.FindAsync(combinedFilter).Result.FirstAsync();

            return result;


        }
        public async Task<TenantsStatistics> GetTenantsMonthStatistics(int year, int month)
        {
            var builder = Builders<TenantsStatistics>.Filter;
            var monthFilter = builder.Eq(a => a.Year.Value, year) & builder.Eq(a => a.Month.Value, month);
            var nullFilter = builder.Eq(a => a.Day, null) & builder.Eq(a => a.Hour, null);
            var combinedFilter = monthFilter & nullFilter;

            var result = await _tenantsStatisticsCollection.FindAsync(combinedFilter).Result.FirstAsync();

            return result;

        }
        public async Task<TenantsStatistics> GetTenantsDayStatistics(int year, int month, int day)
        {
            var builder = Builders<TenantsStatistics>.Filter;
            var dayFilter = builder.Eq(a => a.Year.Value, year) & builder.Eq(a => a.Month.Value, month) & builder.Eq(a => a.Day.Value, day);
            var nullFilter = builder.Eq(a => a.Hour, null);
            var combinedFilter = dayFilter & nullFilter;

            var result = await _tenantsStatisticsCollection.FindAsync(combinedFilter).Result.FirstAsync();

            return result;
        }
        public async Task<TenantsStatistics> GetTenantsHourStatistics(int year, int month, int day, int hour)
        {
            var builder = Builders<TenantsStatistics>.Filter;
            var dayFilter = builder.Eq(a => a.Year.Value, year) & builder.Eq(a => a.Month.Value, month) & builder.Eq(a => a.Day.Value, day) & builder.Eq(a => a.Hour.Value, hour);
            var combinedFilter = dayFilter;

            var result = await _tenantsStatisticsCollection.FindAsync(combinedFilter).Result.FirstAsync();

            return result;
        }
        public async Task<TenantsStatistics> GetTenantsAnyStatistics(int year, int? month, int? day, int? hour)
        {
            var builder = Builders<TenantsStatistics>.Filter;
            var anyFilter = builder.Eq(a => a.Year.Value, year) & builder.Eq(a => a.Month.Value, month) & builder.Eq(a => a.Day.Value, day) & builder.Eq(a => a.Hour.Value, hour);
            var combinedFilter = anyFilter;

            var result = await _tenantsStatisticsCollection.FindAsync(combinedFilter).Result.FirstAsync();

            return result;
        }


        public Task<List<TenantsStatistics>> GetNotProcessedStatistics()
        {
            throw new NotImplementedException();
        }

        public async Task<TenantsStatistics> GetNotSentTenantStatistics()
        {
            var filter = Builders<TenantsStatistics>.Filter.Where(a => a.IsSent == false);
            var response = await _tenantsStatisticsCollection.Find(filter).FirstAsync();
            return response;
        }
    }
}

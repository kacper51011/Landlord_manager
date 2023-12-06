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
    public class ApartmentsStatisticsRepository : IApartmentsStatisticsRepository
    {
        private IMongoCollection<ApartmentsStatistics> _apartmentsStatisticsCollection;
        public ApartmentsStatisticsRepository(IOptions<MongoSettings> databaseSettings)

        {
            var mongoClient = new MongoClient(
                databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                databaseSettings.Value.DatabaseName);

            _apartmentsStatisticsCollection = mongoDatabase.GetCollection<ApartmentsStatistics>(
                databaseSettings.Value.ApartmentsCollectionName);
        }
        public async Task CreateOrUpdateApartmentStatistics(ApartmentsStatistics apartmentsStatistics)
        {
            var result = await _apartmentsStatisticsCollection.ReplaceOneAsync(x => x.ApartmentsStatisticsId == apartmentsStatistics.ApartmentsStatisticsId, apartmentsStatistics, new ReplaceOptions()
            {
                IsUpsert = true
            });

        }

        public async Task<ApartmentsStatistics> GetApartmentStatisticsById(string apartmentStatisticsId)
        {
            return await _apartmentsStatisticsCollection.FindAsync(x => x.ApartmentsStatisticsId == apartmentStatisticsId).Result.FirstAsync();

        }
        public async Task<ApartmentsStatistics> GetApartmentYearStatistics(int year)
        {

            var builder = Builders<ApartmentsStatistics>.Filter;
            var yearFilter = builder.Eq(a => a.Year, year);
            var nullFilter = builder.Eq(a => a.Month, null) & builder.Eq(a => a.Day, null) & builder.Eq(a => a.Hour, null);
            var combinedFilter = yearFilter & nullFilter;

            var result = await _apartmentsStatisticsCollection.FindAsync(combinedFilter).Result.FirstAsync();

            return result;


        }
        public async Task<ApartmentsStatistics> GetApartmentMonthStatistics(int year, int month)
        {
            var builder = Builders<ApartmentsStatistics>.Filter;
            var monthFilter = builder.Eq(a => a.Year, year) & builder.Eq(a => a.Month, month);
            var nullFilter = builder.Eq(a => a.Day, null) & builder.Eq(a => a.Hour, null);
            var combinedFilter = monthFilter & nullFilter;

            var result = await _apartmentsStatisticsCollection.FindAsync(combinedFilter).Result.FirstAsync();

            return result;

        }
        public async Task<ApartmentsStatistics> GetApartmentDayStatistics(int year, int month, int day)
        {
            var builder = Builders<ApartmentsStatistics>.Filter;
            var dayFilter = builder.Eq(a => a.Year, year) & builder.Eq(a => a.Month, month) & builder.Eq(a => a.Day, day);
            var nullFilter = builder.Eq(a => a.Hour, null);
            var combinedFilter = dayFilter & nullFilter;

            var result = await _apartmentsStatisticsCollection.FindAsync(combinedFilter).Result.FirstAsync();

            return result;
        }
        public async Task<ApartmentsStatistics> GetApartmentHourStatistics(int year, int month, int day, int hour)
        {
            var builder = Builders<ApartmentsStatistics>.Filter;
            var dayFilter = builder.Eq(a => a.Year, year) & builder.Eq(a => a.Month, month) & builder.Eq(a => a.Day, day) & builder.Eq(a => a.Hour, hour);
            var combinedFilter = dayFilter;

            var result = await _apartmentsStatisticsCollection.FindAsync(combinedFilter).Result.FirstAsync();

            return result;
        }

        public Task<List<ApartmentsStatistics>> GetNotProcessedStatistics()
        {
            throw new NotImplementedException();
        }

    }
}

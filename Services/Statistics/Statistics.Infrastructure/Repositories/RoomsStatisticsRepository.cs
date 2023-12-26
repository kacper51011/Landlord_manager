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
    public class RoomsStatisticsRepository: IRoomsStatisticsRepository
    {
        private IMongoCollection<RoomsStatistics> _roomsStatisticsCollection;
        public RoomsStatisticsRepository(IOptions<MongoSettings> databaseSettings)

        {
            var mongoClient = new MongoClient(
                databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                databaseSettings.Value.DatabaseName);

            _roomsStatisticsCollection = mongoDatabase.GetCollection<RoomsStatistics>(
                databaseSettings.Value.RoomsCollectionName);
        }
        public async Task CreateOrUpdateRoomsStatistics(RoomsStatistics roomsStatistics)
        {
            var result = await _roomsStatisticsCollection.ReplaceOneAsync(x => x.RoomsStatisticsId == roomsStatistics.RoomsStatisticsId, roomsStatistics, new ReplaceOptions()
            {
                IsUpsert = true
            });

        }

        public async Task<RoomsStatistics> GetRoomsStatisticsById(string apartmentStatisticsId)
        {
            return await _roomsStatisticsCollection.FindAsync(x => x.RoomsStatisticsId == apartmentStatisticsId).Result.FirstAsync();

        }
        public async Task<RoomsStatistics> GetRoomsYearStatistics(int year)
        {

            var builder = Builders<RoomsStatistics>.Filter;
            var yearFilter = builder.Eq(a => a.Year.Value, year);
            var nullFilter = builder.Eq(a => a.Month, null) & builder.Eq(a => a.Day, null) & builder.Eq(a => a.Hour, null);
            var combinedFilter = yearFilter & nullFilter;

            var result = await _roomsStatisticsCollection.FindAsync(combinedFilter).Result.FirstAsync();

            return result;


        }
        public async Task<RoomsStatistics> GetRoomsMonthStatistics(int year, int month)
        {
            var builder = Builders<RoomsStatistics>.Filter;
            var monthFilter = builder.Eq(a => a.Year.Value, year) & builder.Eq(a => a.Month.Value, month);
            var nullFilter = builder.Eq(a => a.Day, null) & builder.Eq(a => a.Hour, null);
            var combinedFilter = monthFilter & nullFilter;

            var result = await _roomsStatisticsCollection.FindAsync(combinedFilter).Result.FirstAsync();

            return result;

        }
        public async Task<RoomsStatistics> GetRoomsDayStatistics(int year, int month, int day)
        {
            var builder = Builders<RoomsStatistics>.Filter;
            var dayFilter = builder.Eq(a => a.Year.Value, year) & builder.Eq(a => a.Month.Value, month) & builder.Eq(a => a.Day.Value, day);
            var nullFilter = builder.Eq(a => a.Hour, null);
            var combinedFilter = dayFilter & nullFilter;

            var result = await _roomsStatisticsCollection.FindAsync(combinedFilter).Result.FirstAsync();

            return result;
        }
        public async Task<RoomsStatistics> GetRoomsHourStatistics(int year, int month, int day, int hour)
        {
            var builder = Builders<RoomsStatistics>.Filter;
            var dayFilter = builder.Eq(a => a.Year.Value, year) & builder.Eq(a => a.Month.Value, month) & builder.Eq(a => a.Day.Value, day) & builder.Eq(a => a.Hour.Value, hour);
            var combinedFilter = dayFilter;

            var result = await _roomsStatisticsCollection.FindAsync(combinedFilter).Result.FirstAsync();

            return result;
        }
        public async Task<RoomsStatistics> GetRoomsAnyStatistics(int year, int? month, int? day, int? hour)
        {
            var builder = Builders<RoomsStatistics>.Filter;
            var anyFilter = builder.Eq(a => a.Year.Value, year) & builder.Eq(a => a.Month.Value, month) & builder.Eq(a => a.Day.Value, day) & builder.Eq(a => a.Hour.Value, hour);
            var combinedFilter = anyFilter;

            var result = await _roomsStatisticsCollection.FindAsync(combinedFilter).Result.FirstAsync();

            return result;
        }


        public Task<List<RoomsStatistics>> GetNotProcessedStatistics()
        {
            throw new NotImplementedException();
        }

        public async Task<RoomsStatistics> GetNotSentRoomStatistics()
        {
            var filter = Builders<RoomsStatistics>.Filter.Where(a => a.IsSent == false);
            var response = await _roomsStatisticsCollection.Find(filter).FirstAsync();
            return response;
        }
    }
}

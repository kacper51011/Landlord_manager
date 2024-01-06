using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Rooms.Domain;
using Rooms.Domain.Interfaces;
using Rooms.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rooms.Infrastructure.Repositories
{
    public class RoomsStatisticsRepository: IRoomsStatisticsRepository
    {
        private IMongoCollection<RoomsStatistics> _roomsStatisticsCollection;
        public RoomsStatisticsRepository(IOptions<MongoSettings> roomsDatabaseSettings)
        {
            var mongoClient = new MongoClient(
    roomsDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                roomsDatabaseSettings.Value.DatabaseName);

            _roomsStatisticsCollection = mongoDatabase.GetCollection<RoomsStatistics>(
                roomsDatabaseSettings.Value.CollectionStatisticsName);

        }
        public async Task CreateOrUpdateRoomStatistics(RoomsStatistics roomsStatistics)
        {
            await _roomsStatisticsCollection.ReplaceOneAsync(x => x.RoomsStatisticsId == roomsStatistics.RoomsStatisticsId, roomsStatistics, new ReplaceOptions()
            {
                IsUpsert = true
            });
        }

        public async Task<RoomsStatistics> GetRoomStatisticsById(string apartmentStatisticsId)
        {
            return await _roomsStatisticsCollection.FindAsync(x => x.RoomsStatisticsId == apartmentStatisticsId).Result.FirstAsync();

        }
        public async Task<RoomsStatistics> GetUnproccessedRoomStatistics()
        {
            var builder = Builders<RoomsStatistics>.Filter;
            var filter = builder.Eq(x => x.AreInformationsSubmitted, false);
            var sort = Builders<RoomsStatistics>.Sort.Ascending(x => x.LastModifiedDate);

            var returnValue = await _roomsStatisticsCollection.Find(filter).Sort(sort).FirstOrDefaultAsync();
            return returnValue;
        }
        public async Task<RoomsStatistics> GetNotSendRoomsStatistics()
        {
            var builder = Builders<RoomsStatistics>.Filter;
            var filter = builder.Eq(x => x.IsSendToStatisticsService, false);

            var sort = Builders<RoomsStatistics>.Sort.Descending(x => x.LastModifiedDate);
            var returnValue = await _roomsStatisticsCollection.Find(filter).Sort(sort).FirstOrDefaultAsync();
            return returnValue;
        }

        public async Task<RoomsStatistics> GetRoomAnyStatistics(int year, int? month, int? day, int? hour)
        {
            var builder = Builders<RoomsStatistics>.Filter;
            var anyFilter = builder.Eq(a => a.Year.Value, year) & builder.Eq(a => a.Month.Value, month) & builder.Eq(a => a.Day.Value, day) & builder.Eq(a => a.Hour.Value, hour);
            var combinedFilter = anyFilter;

            var result = await _roomsStatisticsCollection.FindAsync(combinedFilter).Result.FirstAsync();

            return result;
        }
    }
}


using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Rooms.Domain.Entities;
using Rooms.Domain.Interfaces;
using Rooms.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rooms.Infrastructure.Repositories
{
    public class RoomsRepository : IRoomsRepository
    {
        private IMongoCollection<Room> _roomsCollection;
        public RoomsRepository(IOptions<MongoSettings> roomsDatabaseSettings)
        {
            var mongoClient = new MongoClient(roomsDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(roomsDatabaseSettings.Value.DatabaseName);

            _roomsCollection = mongoDatabase.GetCollection<Room>(roomsDatabaseSettings.Value.CollectionName);
        }
        public async Task CreateOrUpdateRoom(Room room)
        {
            await _roomsCollection.ReplaceOneAsync(x => x.RoomId == room.RoomId, room, new ReplaceOptions()
            {
                IsUpsert = true
            });
        }

        public async Task DeleteAllRoomsInRoom(string landlordId, string apartmentId)
        {
            var builder = Builders<Room>.Filter;
            var filter = builder.Eq(a => a.LandlordId, landlordId) & builder.Eq(a => a.RoomId, apartmentId);
            await _roomsCollection.DeleteManyAsync(filter);
        }

        public async Task DeleteRoom(string roomId)
        {
            await _roomsCollection.FindOneAndDeleteAsync(x => x.RoomId == roomId);
        }
        public async Task<List<Room>> GetOldestCheckedRooms()
        {

            var builder = Builders<Room>.Filter;

            var filter = builder.Lte(x => x.LastCheckedDate, DateTime.UtcNow);

            var options = new FindOptions<Room>()
            {
                Sort = Builders<Room>.Sort.Descending(x => x.LastCheckedDate),
                BatchSize = 5,
            };

            return await _roomsCollection.FindAsync(filter, options).Result.ToListAsync();
        }

        public async Task<Room> GetRoomById(string roomId)
        {
            try
            {
                return await _roomsCollection.FindAsync(x => x.RoomId == roomId).Result.FirstAsync();
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<Room> GetRoomByIdAndLandlordId(string landlordId, string roomId)
        {
            var builder = Builders<Room>.Filter;
            var filter = builder.Eq(a => a.RoomId, roomId) & builder.Eq(a => a.LandlordId, landlordId);

            return await _roomsCollection.FindAsync(filter).Result.FirstAsync();
        }

        public async Task<List<Room>> GetRoomsByRoomId(string apartmentId)
        {
            return await _roomsCollection.FindAsync(x => x.RoomId == apartmentId).Result.ToListAsync();
        }

    public async Task<int> GetMostRoomsInOneApartment(DateTime endDate)
    {
        var filter = Builders<Room>.Filter.Lte(a => a.CreationDate, endDate);
        var aggregation = _roomsCollection.Aggregate()
        .Match(filter)
        .Group(
            key => key.LandlordId,
            group => new
            {
                LandlordId = group.Key,
                objectsCount = group.Count()
            }
            )
        .SortBy(x => x.objectsCount);
        var result = await aggregation.ToListAsync();
        return result[0].objectsCount;
    }
    public async Task<int> GetUpdatedRoomsCount(DateTime startDate, DateTime endDate)
    {
        var builder = Builders<Room>.Filter;
        var dateRangeFilter = builder.Gte(a => a.UpdateDates.Min(), startDate) & builder.Lte(a => a.UpdateDates.Max(), endDate);
        var versionFilter = builder.Gt(a => a.Version, 1);
        var combinedFilter = versionFilter & dateRangeFilter;

        var result = await _roomsCollection.CountDocumentsAsync(combinedFilter);

        return (int)result;
    }
    public async Task<int> GetCreatedRoomsCount(DateTime startDate, DateTime endDate)
    {
        //filter for number of created rooms from start - end date
        var builder = Builders<Room>.Filter;
        var filter = builder.Gte(a => a.CreationDate, startDate) & builder.Lt(a => a.CreationDate, endDate);

        var result = await _roomsCollection.CountDocumentsAsync(filter);


        return (int)result;

    }

        public async Task<List<Room>> GetRoomsByApartmentId(string apartmentId)
        {
            var builder = Builders<Room>.Filter;
            var filter = builder.Eq(r => r.ApartmentId, apartmentId);

            var result = await _roomsCollection.Find(filter).ToListAsync();
            return result;
        }

        public async Task<int> GetBiggestCreatedRoomSize(DateTime dateEnd)
        {
            var builder = Builders<Room>.Filter;
            var filter = builder.Lt(a => a.CreationDate, dateEnd);
            var sort = Builders<Room>.Sort.Descending(x => x.Surface);

            var result = await _roomsCollection.Find(filter).Sort(sort).FirstAsync();

            return result.Surface;
        }
    }

}

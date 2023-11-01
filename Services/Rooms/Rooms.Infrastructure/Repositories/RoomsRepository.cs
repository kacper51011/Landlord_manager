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

        public async Task DeleteAllRoomsInApartment(string landlordId, string apartmentId)
        {
            var builder = Builders<Room>.Filter;
            var filter = builder.Eq(a => a.LandlordId, landlordId) & builder.Eq(a => a.ApartmentId, apartmentId);
            await _roomsCollection.DeleteManyAsync(filter);
        }

        public async Task DeleteRoom(string roomId)
        {
            await _roomsCollection.FindOneAndDeleteAsync(x => x.RoomId == roomId);
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

        public async Task<List<Room>> GetRoomsByApartmentId(string apartmentId)
        {
            return await _roomsCollection.FindAsync(x => x.ApartmentId == apartmentId).Result.ToListAsync();
        }
    }
}

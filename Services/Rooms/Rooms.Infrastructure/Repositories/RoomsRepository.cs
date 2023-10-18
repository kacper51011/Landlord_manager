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
            var mongoClient = new MongoClient(
    roomsDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                roomsDatabaseSettings.Value.DatabaseName);

            _roomsCollection = mongoDatabase.GetCollection<Room>(
                roomsDatabaseSettings.Value.CollectionName);
        }
        public Task CreateOrUpdateRoom(Room room)
        {
            
        }

        public Task DeleteApartment(string apartmentId)
        {
            throw new NotImplementedException();
        }

        public Task<Room> GetApartmentById(string apartmentId)
        {
            throw new NotImplementedException();
        }

        public Task<Room> GetRoomByIdAndLandlordId(string landlordId, string apartmentId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Room>> GetRoomsByApartmentId(string apartmentId)
        {
            throw new NotImplementedException();
        }
    }
}

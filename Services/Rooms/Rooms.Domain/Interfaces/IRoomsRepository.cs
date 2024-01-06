using Rooms.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rooms.Domain.Interfaces
{
    public interface IRoomsRepository
    {
        public Task<List<Room>> GetRoomsByApartmentId(string apartmentId);
        public Task CreateOrUpdateRoom(Room room);
        public Task<Room> GetRoomByIdAndLandlordId(string landlordId, string roomId);
        public Task DeleteRoom(string roomId);
        public Task<List<Room>> GetOldestCheckedRooms();
        public Task DeleteAllRoomsInApartment(string landlordId, string apartmentId);
        public Task<Room> GetRoomById(string roomId);

        //gets for statistics
        public Task<int> GetRoomsCreated(DateTime dateStart, DateTime dateEnd);
        public Task<int> GetRoomsUpdated(DateTime dateStart, DateTime dateEnd);
        public Task<int> GetMostRoomsInOneApartment(DateTime dateEnd);
        public Task<int> GetBiggestCreatedRoomSize(DateTime dateEnd);
    }
}

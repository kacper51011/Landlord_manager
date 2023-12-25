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
    }
}

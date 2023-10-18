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
        public Task<Room> GetRoomByIdAndLandlordId(string landlordId, string apartmentId);
        public Task DeleteApartment(string apartmentId);
        public Task<Room> GetApartmentById(string apartmentId);
    }
}

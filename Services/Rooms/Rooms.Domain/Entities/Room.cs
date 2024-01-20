using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rooms.Domain.Entities
{
    public class Room: AggregateRoot
    {
        private Room() { }
        public string RoomId { get; private set; }
        public string ApartmentId { get; private set; }
        public string LandlordId { get; private set; }
        public string Name { get; private set; }
        public int Surface { get; private set; }
        public string AnglesCoordinates { get; private set; }
        public List<DateTime> UpdateDates { get; private set; }
        public DateTime LastCheckedDate { get; private set; }

        public static Room CreateRoom(string apartmentId, string landlordId, string name, int surface, string anglesCoordinates)
        {
            Room room = new Room()
            {
                RoomId = Guid.NewGuid().ToString(),
                ApartmentId = apartmentId,
                LandlordId = landlordId,
                LastCheckedDate = DateTime.UtcNow,
                Name = name,
                Surface = surface,
                AnglesCoordinates = anglesCoordinates,
            };
            room.SetCreationDate();
            room.SetLastModifiedDate();
            room.SetLastCheckedDate();
            room.IncrementVersion();
            return room;
        }
        public void UpdateRoom(string name, int surface, string anglesCoordinates)
        {
            Name = name;
            Surface = surface;
            AnglesCoordinates = anglesCoordinates;
            SetNewUpdateDate();
            IncrementVersion();
            SetLastModifiedDate();
        }

        public void SetLastCheckedDate()       
        {
            LastCheckedDate = DateTime.UtcNow;
        }

        public void SetNewUpdateDate()
        {
            UpdateDates.Add(DateTime.UtcNow);
        }


    }
}

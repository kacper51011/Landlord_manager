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
        public string Name { get; private set; }
        public int Surface { get; private set; }
        public string AnglesCoordinates { get; private set; }
        public int MaxTenantsNumber { get; private set; }
        public int CurrentTenantsNumber { get; private set; }
        public int MonthlyRent { get; private set; }

        public static Room CreateRoom(string apartmentId, string name, int surface, string anglesCoordinates, int maxTenantsNumber, int currentTenantsNumber, int monthlyRent)
        {
            Room room = new Room()
            {
                RoomId = Guid.NewGuid().ToString(),
                ApartmentId = apartmentId,
                Name = name,
                Surface = surface,
                AnglesCoordinates = anglesCoordinates,
                MaxTenantsNumber = maxTenantsNumber,
                CurrentTenantsNumber = currentTenantsNumber,
                MonthlyRent = monthlyRent
            };
            room.SetCreationDate();
            room.SetLastModifiedDate();
            return room;
        }
        public Room UpdateRoom(string name, int surface, string anglesCoordinates, int maxTenantsNumber, int currentTenantsNumber, int monthlyRent)
        {
            Name = name;
            Surface = surface;
            AnglesCoordinates = anglesCoordinates;
            MaxTenantsNumber = maxTenantsNumber;
            CurrentTenantsNumber = currentTenantsNumber;
            MonthlyRent = monthlyRent;
            SetLastModifiedDate();
            return this;
        }
    }
}

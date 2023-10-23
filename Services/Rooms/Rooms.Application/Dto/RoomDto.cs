using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rooms.Application.Dto
{
    public class RoomDto
    {
        public string? RoomId { get; private set; }
        public string ApartmentId { get; private set; }
        public string LandlordId { get; private set; }
        public string Name { get; private set; }
        public int Surface { get; private set; }
        public string AnglesCoordinates { get; private set; }
        public int MaxTenantsNumber { get; private set; }
        public int CurrentTenantsNumber { get; private set; }
        public int MonthlyRent { get; private set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rooms.Application.Dto
{
    public class RoomDto
    {
        public string? RoomId { get; set; }
        public string ApartmentId { get; set; }
        public string LandlordId { get; set; }
        public string Name { get; set; }
        public int Surface { get; set; }
        public string AnglesCoordinates { get; set; }
        public int MaxTenantsNumber { get; set; }
        public int CurrentTenantsNumber { get; set; }
        public int MonthlyRent { get; set; }
    }
}

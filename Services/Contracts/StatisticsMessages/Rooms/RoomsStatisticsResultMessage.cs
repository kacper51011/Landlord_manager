using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.StatisticsMessages.Rooms
{
    public record RoomsStatisticsResultMessage
    {
        public int RoomsCreated { get; set; }
        public int RoomsUpdated { get;  set; }
        public int BiggestCreatedRoomSize { get; set; }
        public int MostRoomsInApartment { get; set; }
        public int Year { get; set; }
        public int? Month { get; set; }
        public int? Day { get; set; }
        public int? Hour { get; set; }
    }
}

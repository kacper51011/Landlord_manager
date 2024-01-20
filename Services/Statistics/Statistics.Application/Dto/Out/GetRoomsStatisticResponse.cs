using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Application.Dto.Out
{
    public class GetRoomsStatisticResponse
    {
        public string RoomStatisticsId { get; set; }
        public DateTime StatisticsStart { get; set; }
        public DateTime StatisticsEnd { get; set; }
        public int RoomsCreated { get; set; }
        public int RoomsUpdated { get; set; }
        public int BiggestCreatedRoomSize { get; set; }
        public int MostRoomsInApartment { get; set; }
    }
}

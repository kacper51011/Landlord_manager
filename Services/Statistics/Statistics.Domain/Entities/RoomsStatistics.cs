using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Domain.Entities
{
    public class RoomsHourStatistics: AggregateRoot
    {
        public string RoomsHourStatisticsId { get; private set; }
        //public string StatisticsScope { get; private set; }
        public DateTime StatisticsStart { get; private set; }
        public DateTime StatisticsEnd { get; private set; }

        public int RoomsCreated { get; private set; }
        public int RoomsUpdated { get; private set; }
        public int BiggestRoom { get; private set; }
        public int MostRoomsInApartment { get; private set; }
    }
}

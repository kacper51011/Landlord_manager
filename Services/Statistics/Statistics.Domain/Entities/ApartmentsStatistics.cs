using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Domain.Entities
{
    public class ApartmentsStatistics: AggregateRoot
    {
        public string ApartmentsStatisticsId { get; private set; }
        //public string StatisticsScope { get; private set; }
        public DateTime StatisticsStart { get; private set; }
        public DateTime StatisticsEnd { get; private set; }
        public int ApartmentsCreated { get; private set; }
        public int ApartmentsUpdated { get; private set; }
        public int MostApartmentsOwnedByUser { get; private set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Domain.Entities
{
    public class TenantsStatistics: AggregateRoot
    {
        public string TenantsStatisticsId { get; private set; }        
        // AllTime, Year, Month, 
        //public string StatisticsScope { get; private set; }
        public DateTime StatisticsStart { get; private set; }
        public DateTime StatisticsEnd { get; private set; }
        public int TenantsCreated { get; private set; }
        public int TenantsUpdated { get; private set; }
        public int HighestRent { get; private set; }
        public int MostTenantsInOneRoom { get; private set; }

    }
}

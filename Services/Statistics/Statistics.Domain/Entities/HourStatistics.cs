using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Domain.Entities
{
    public class HourStatistics: AggregateRoot
    {
        public string HourStatisticsId { get; private set; }
        public int UsersCreated { get; private set; }
        public int ApartmentsCreated { get; private set; }
        public int RoomsCreated { get; private set; }
        public int TenantsCreated { get; private set; }
    }
}

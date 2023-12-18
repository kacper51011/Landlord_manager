using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.StatisticsMessages.Tenants
{
    public record TenantsStatisticsResultMessage
    {
        public int Year { get; set; }
        public int? Month { get; set; }
        public int? Day { get; set; }
        public int? Hour { get; set; }
        public int TenantsCreated { get; set; }
        public int TenantsUpdated { get; set; }
        public int HighestRent { get; set; }
        public int MostTenantsInRoom { get; set; }
    }
}

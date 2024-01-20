using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Application.Dto.Out
{
    public class GetTenantsStatisticResponse
    {
        public string tenantsStatisticId {  get; set; }
        public int Year { get; set; }
        public int? Month { get; set; }
        public int? Day { get; set; }
        public int? Hour { get; set; }
        public DateTime StatisticsStart { get; set; }
        public DateTime StatisticsEnd { get; set; }
        public int TenantsCreated { get; set; }
        public int TenantsUpdated { get; set; }
        public int HighestRent { get; set; }
        public int MostTenantsInRoom { get; set; }
    }
}

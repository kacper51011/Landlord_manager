using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Application.Dto.Out
{
    public class GetApartmentsStatisticResponse
    {
        public string ApartmentsStatisticsId { get; set; }
        public DateTime StatisticsStart { get; set; }
        public DateTime StatisticsEnd { get; set; }
        public int ApartmentsCreated { get; set; }
        public int ApartmentsUpdated { get; set; }
        public int MostApartmentsOwnedByUser { get; set; }
    }
}

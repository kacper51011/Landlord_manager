using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.StatisticsMessages.Apartments
{
    public record ApartmentsStatisticsResultMessage
    {
        public int ApartmentsCreated { get; set; }
        public int ApartmentsUpdated { get; set; }
        public int MostApartmentsOwnedByUser { get; set; }
    }
}

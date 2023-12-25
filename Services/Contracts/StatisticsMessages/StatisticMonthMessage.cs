using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.StatisticsMessages
{
    public record StatisticMonthMessage
    {
        public int Year { get; set; }
        public int Month { get; set; }
    }
}

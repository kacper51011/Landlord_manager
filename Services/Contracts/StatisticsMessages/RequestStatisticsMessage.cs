using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.StatisticsMessages
{
    public record RequestStatisticsMessage
    {
        public DateTime TimeStart {  get; set; }

        public DateTime TimeEnd { get; set; }

    }
}

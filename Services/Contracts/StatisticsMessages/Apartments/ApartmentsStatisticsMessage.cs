﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.StatisticsMessages.Apartments
{
    public record ApartmentsStatisticsMessage
    {
        public string StatisticsId { get; set; }
        public DateTime StatisticsStart { get; set; }
        public DateTime StatisticsEnd { get; set; }
    }
}

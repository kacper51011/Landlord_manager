using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Application.Dto.In
{
    public class GetAnyStatisticDto
    {
        public int Year { get; set; }
        public int? Month { get; set; }
        public int? Day { get; set; }
        public int? Hour { get; set; }
        public string Type { get; set; }
        public string Scope { get; set; }
    }

}

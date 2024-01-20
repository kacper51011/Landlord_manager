using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rooms.Domain.ValueObjects
{
    public class StatisticsEnd
    {
        public DateTime Value { get; }
        public StatisticsEnd(DateTime value)
        {
            Validate(value);
            Value = value;

        }
        private void Validate(DateTime value)
        {
            if (value > DateTime.UtcNow.AddHours(1))
            {
                throw new ArgumentOutOfRangeException(nameof(value), "You can`t request that Date yet ");
            }

        }
    }
}

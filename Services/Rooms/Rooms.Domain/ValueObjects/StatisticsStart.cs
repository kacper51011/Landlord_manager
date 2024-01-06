using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rooms.Domain.ValueObjects
{
    public class StatisticsStart
    {
        public DateTime Value { get; }
        private readonly DateTime serviceStart = new(2023, 12, 15, 15, 1, 1);
        public StatisticsStart(DateTime value)
        {
            Validate(value);
            Value = value;

        }
        private void Validate(DateTime value)
        {
            if (value < serviceStart)
            {
                throw new ArgumentOutOfRangeException(nameof(value), $"Servers are running from ${serviceStart}, try later dates");
            }
        }
    }
}

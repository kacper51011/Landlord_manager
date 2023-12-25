using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apartments.Domain.ValueObjects
{
    public class Day
    {
        public int Value { get; }

        public Day(int value)
        {
            Validate(value);
            Value = value;
        }

        private void Validate(int value)
        {
            if (value < 1 || value > 31)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Day must be between 1 and 31.");
            }
        }
    }
}

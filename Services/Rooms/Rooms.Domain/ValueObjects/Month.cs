using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rooms.Domain.ValueObjects
{
    public class Month
    {
        public int Value { get; }

        public Month(int value)
        {
            Validate(value);
            Value = value;
        }

        private void Validate(int value)
        {
            if (value < 1 || value > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Month must be between 1 and 12.");
            }
        }
    }
}

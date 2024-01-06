using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rooms.Domain.ValueObjects
{
    public class Hour
    {
        public int Value { get; }

        public Hour(int value)
        {
            Validate(value);
            Value = value;
        }

        private void Validate(int value)
        {
            if (value < 0 || value > 23)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Hour must be between 0 and 23.");
            }
        }
    }
}

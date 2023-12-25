using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apartments.Domain.ValueObjects
{
    public class Year
    {
        public int Value { get; }

        public Year(int value)
        {
            Validate(value);
            Value = value;
        }

        private void Validate(int value)
        {
            if (value < 2023)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Year statistics Starts from 2023");
            }
        }
    }
}

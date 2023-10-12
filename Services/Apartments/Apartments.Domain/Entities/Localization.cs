using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apartments.Domain.Entities
{
    public class Localization
    {
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }

        public Localization(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}

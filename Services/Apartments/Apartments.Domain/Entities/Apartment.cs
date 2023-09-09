using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apartments.Domain.Entities
{
    public class Apartment
    {
        public string Id { get; set; }
        public Localization Localization { get; set; }

        public string LandlordId { get; set; }
        public string Flatmates { get; set; }
        public string ApartmentProblems { get; set; }

        public string Telephone { get; set; }

        public string Rooms { get; set; }
        public int Surface { get; set; }
        public string Images { get; set; }
    }
    public class Localization 
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    
    }
}

using Apartments.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apartments.Application.Queries.GetApartments
{
    public class GetApartmentsQueryDto
    {
        public string Id { get; set; }
        public LocalizationDto Localization { get; set; }
        public string LandlordId { get; set; }
        public List<string> Flatmates { get; set; }
        public List<string> ApartmentProblems { get; set; }
        public string Telephone { get; set; }
        public string Rooms { get; set; }
        public int Surface { get; set; }
        public string Images { get; set; }
    }
    public class LocalizationDto
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

}

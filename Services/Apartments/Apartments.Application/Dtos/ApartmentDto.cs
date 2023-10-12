using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apartments.Application.Dtos
{
    public class ApartmentDto
    {
        public string? Id { get; set; }
        public LocalizationDto Localization { get; set; }
        public SurfaceInfoDto SurfaceInfo { get; set; }
        public string LandlordId { get; set; }
        public string Telephone { get; set; }
    }
    public class LocalizationDto
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
    public class SurfaceInfoDto
    {
        public int RoomsNumber { get; private set; }
        public int Area { get; private set; }
    }

}

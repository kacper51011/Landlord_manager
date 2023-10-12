using Apartments.Application.Queries.GetApartments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apartments.Domain.Entities
{
    public class Apartment: AggregateRoot
    {
        private Apartment()
        {
            
        }
        
        public string LandlordId { get; private set; }
        public Localization Localization { get; private set; }
        public SurfaceInfo SurfaceInformation { get; private set; }
        public string? Telephone { get; private set; }

        public static Apartment CreateApartment(string landlordId, Localization localization, SurfaceInfo surfaceInformation,  string telephone)
        {
            
            var apartment = new Apartment
            {
                LandlordId = landlordId,
                Localization = localization,
                Telephone = telephone,
                SurfaceInformation = surfaceInformation

            };
            apartment.SetCreationDate();
            apartment.SetLastModifiedDate();
            return apartment;
        }
        public static Apartment UpdateApartment(Apartment apartment, Localization localization, SurfaceInfo surfaceInformation, string telephone)
        {
            apartment.Localization = localization;
            apartment.Telephone = telephone;
            apartment.SurfaceInformation = surfaceInformation;
            apartment.SetLastModifiedDate();
            return apartment;
        }

        
    }

    
    
}

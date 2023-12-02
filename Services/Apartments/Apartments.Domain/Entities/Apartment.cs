
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Apartments.Domain.Entities
{
    public class Apartment: AggregateRoot
    {
        public string ApartmentId { get; private set; }
        public string LandlordId { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public int Area { get; private set; }
        public string Telephone { get; private set; }
        public List<DateTime> UpdateDates{ get; private set; }

        public static Apartment CreateApartment(string landlordId, double latitude, double longitude, int area,  string telephone)
        {

            var apartment = new Apartment
            {
                ApartmentId = Guid.NewGuid().ToString(),
                LandlordId = landlordId,
                Latitude = latitude,
                Longitude = longitude,
                Area = area,
                Telephone = telephone,
                UpdateDates = new List<DateTime>()
                

            };
            apartment.SetCreationDate();
            apartment.SetLastModifiedDate();
            apartment.IncrementVersion();
            return apartment;
        }
        public Apartment UpdateApartment(string landlordId, double latitude, double longitude, int area, string telephone)
        {
            LandlordId = landlordId;
            Latitude = latitude;
            Longitude = longitude;
            Area = area;
            Telephone = telephone;
            SetLastModifiedDate();
            SetNewUpdateDate();
            IncrementVersion();
            return this;
        }

        public void SetNewUpdateDate()
        {
            UpdateDates.Add(DateTime.UtcNow);
        }
        
    }

    
    
}

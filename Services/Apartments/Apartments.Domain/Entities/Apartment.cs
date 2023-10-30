
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apartments.Domain.Entities
{
    public class Apartment: AggregateRoot
    {
        public string ApartmentId { get; private set; }
        public string LandlordId { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public int RoomsNumber { get; private set; }
        public int Area { get; private set; }
        public string Telephone { get; private set; }

        public static Apartment CreateApartment(string landlordId, double latitude, double longitude, int roomsNumber, int area,  string telephone)
        {

            var apartment = new Apartment
            {
                ApartmentId = Guid.NewGuid().ToString(),
                LandlordId = landlordId,
                Latitude = latitude,
                Longitude = longitude,
                RoomsNumber = 0,
                Area = area,
                Telephone = telephone,
                

            };
            apartment.SetCreationDate();
            apartment.SetLastModifiedDate();
            return apartment;
        }
        public Apartment UpdateApartment(string landlordId, double latitude, double longitude, int roomsNumber, int area, string telephone)
        {
            LandlordId = landlordId;
            Latitude = latitude;
            Longitude = longitude;
            RoomsNumber = roomsNumber;
            Area = area;
            Telephone = telephone;
            SetLastModifiedDate();
            return this;
        }

        public void IncreaseRoomNumber()
        {
            RoomsNumber += 1;
        }

        public void DecreaseRoomNumber()
        {
            RoomsNumber -= 1;
        }

        
    }

    
    
}

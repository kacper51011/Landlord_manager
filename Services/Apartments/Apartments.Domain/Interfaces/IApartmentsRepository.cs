using Apartments.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apartments.Domain.Interfaces
{
    public interface IApartmentsRepository
    {
        public Task<List<Apartment>> GetApartmentsByUserId(string landlordId);
        public Task CreateOrUpdateApartment(Apartment apartment);
        public  Task<Apartment> GetApartmentByIdAndLandlordId(string landlordId, string apartmentId);
        public Task DeleteApartment(string apartmentId);
        public Task<Apartment> GetApartmentById(string apartmentId);

        //Statistics
        public Task<int> GetMostApartmentsOwnedByOneUser();

        public Task<List<Apartment>> GetUpdatedApartments();
        public Task<List<Apartment>> GetCreatedApartments(DateTime startDate, DateTime endDate);



    }
}

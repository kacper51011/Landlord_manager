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

        public  Task<Apartment> GetApartmentByIdAndLandlordId(string landlordId, string apartmentId);
        public Task DeleteApartment(string apartmentId);
        public Task<Apartment> GetApartmentById(string apartmentId);
        public Task CreateOrUpdateApartment(Apartment apartment);
        //Statistics
        public Task<int> GetMostApartmentsOwnedByOneUserCount(DateTime endDate);

        public Task<int> GetUpdatedApartmentsCount(DateTime startDate, DateTime endDate);
        public Task<int> GetCreatedApartmentsCount(DateTime startDate, DateTime endDate);



    }
}

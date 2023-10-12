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
        public Task DeleteApartment(string landlordId, string apartmentId);


    }
}

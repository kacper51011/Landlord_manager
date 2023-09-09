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
        public Task<Apartment> GetApartmentsById(string landlordId);
        public Task<Apartment> CreateApartment(Apartment apartment);
        public Task<Apartment> UpdateApartment(Apartment apartment);
        public Task<Apartment> DeleteApartment(string landlordId);


    }
}

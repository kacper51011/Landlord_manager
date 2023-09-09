using Apartments.Domain.Entities;
using Apartments.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apartments.Infrastructure.Repositories
{
    public class ApartmentsRepository : IApartmentsRepository
    {
        public Task<Apartment> CreateApartment(Apartment apartment)
        {
            throw new NotImplementedException();
        }

        public Task<Apartment> DeleteApartment(string landlordId)
        {
            throw new NotImplementedException();
        }

        public Task<Apartment> GetApartmentsById(string landlordId)
        {
            throw new NotImplementedException();
        }

        public Task<Apartment> UpdateApartment(Apartment apartment)
        {
            throw new NotImplementedException();
        }
    }
}

using Landlords.Domain.Entities;
using Landlords.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Landlords.Infrastructure.Repository
{
    public class LandlordsRepository : ILandlordsRepository
    {
        public Task<Landlord> CreateLandlord(LandlordDto landlordDto)
        {
            throw new NotImplementedException();
        }

        public Task<Landlord> GetLandlordById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Landlord> UpdateLandlord(LandlordDto landlordDto)
        {
            throw new NotImplementedException();
        }
    }
}

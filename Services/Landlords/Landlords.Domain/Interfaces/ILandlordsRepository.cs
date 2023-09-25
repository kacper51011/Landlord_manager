using Landlords.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Landlords.Domain.Interfaces
{
    public interface ILandlordsRepository
    {
        Task<Landlord> CreateLandlord(LandlordDto landlordDto);
        Task<Landlord> UpdateLandlord(LandlordDto landlordDto);
        Task<Landlord> GetLandlordById(int id);
    }
}

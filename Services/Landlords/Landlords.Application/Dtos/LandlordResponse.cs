using Landlords.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Landlords.Application.Dtos
{
    public class LandlordResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
    }
    public static class LandlordExtensions
    {
        public static LandlordResponse ToLandlordResponse(this Landlord landlord)
        {
            return new LandlordResponse
            {
                Id = landlord.Id,
                Name = landlord.Name,
                Age = landlord.Age,
                Email = landlord.Email,
            };
        }
    }
}

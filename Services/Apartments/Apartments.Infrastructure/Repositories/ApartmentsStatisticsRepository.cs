using Apartments.Domain;
using Apartments.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apartments.Infrastructure.Repositories
{
    public class ApartmentsStatisticsRepository : IApartmentsStatisticsRepository
    {
        public ApartmentsStatisticsRepository()
        {
            
        }
        public Task CreateOrUpdateApartmentStatistics(ApartmentsHourStatistics apartment)
        {
            throw new NotImplementedException();
        }

        public Task<ApartmentsHourStatistics> GetApartmentStatisticsById(string apartmentStatisticsId)
        {
            throw new NotImplementedException();
        }
    }
}

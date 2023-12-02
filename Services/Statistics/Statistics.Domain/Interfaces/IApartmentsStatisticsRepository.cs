using Statistics.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Domain.Interfaces
{
    public interface IApartmentsStatisticsRepository
    {
        public Task<ApartmentsStatistics> GetApartmentStatisticsById(string apartmentStatisticsId);
        public Task CreateOrUpdateApartmentStatistics(ApartmentsStatistics apartment);
    }
}

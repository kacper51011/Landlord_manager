using Apartments.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apartments.Domain.Interfaces
{
    public interface IApartmentsStatisticsRepository
    {
        public Task<ApartmentsStatistics> GetApartmentStatisticsById(string apartmentStatisticsId);
        public Task CreateOrUpdateApartmentStatistics(ApartmentsStatistics apartment);
        public Task<ApartmentsStatistics> GetNotSendApartmentsStatistics();

        public Task<ApartmentsStatistics> GetUnproccessedApartmentStatistics();

    }
}

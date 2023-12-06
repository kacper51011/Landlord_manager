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
        public Task<List<ApartmentsStatistics>> GetNotProcessedStatistics();

        //gets
        public Task<ApartmentsStatistics> GetApartmentHourStatistics(int year, int month, int day, int hour);
        public Task<ApartmentsStatistics> GetApartmentDayStatistics(int year, int month, int day);
        public Task<ApartmentsStatistics> GetApartmentMonthStatistics(int year, int month);
        public Task<ApartmentsStatistics> GetApartmentYearStatistics(int year);





    }
}

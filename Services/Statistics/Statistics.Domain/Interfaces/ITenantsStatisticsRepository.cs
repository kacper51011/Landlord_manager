using Statistics.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Domain.Interfaces
{
    public interface ITenantsStatisticsRepository
    {
        public Task<TenantsStatistics> GetTenantsStatisticsById(string tenantsStatisticsId);
        public Task CreateOrUpdateTenantsStatistics(TenantsStatistics tenantsStatistics);
        public Task<List<TenantsStatistics>> GetNotProcessedStatistics();

        //gets
        public Task<TenantsStatistics> GetTenantsHourStatistics(int year, int month, int day, int hour);
        public Task<TenantsStatistics> GetTenantsDayStatistics(int year, int month, int day);
        public Task<TenantsStatistics> GetTenantsMonthStatistics(int year, int month);
        public Task<TenantsStatistics> GetTenantsYearStatistics(int year);
        public Task<TenantsStatistics> GetTenantsAnyStatistics(int year, int? month, int? day, int? hour);

        public Task<TenantsStatistics> GetNotSentTenantStatistics();

    }
}

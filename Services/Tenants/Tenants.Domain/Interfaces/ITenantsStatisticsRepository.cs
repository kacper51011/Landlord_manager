using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenants.Domain.Interfaces
{
    public interface ITenantsStatisticsRepository
    {
        public Task<TenantsStatistics> GetTenantsStatisticsById(string tenantStatisticsId);
        public Task CreateOrUpdateTenantsStatistics(TenantsStatistics tenant);
        public Task<TenantsStatistics> GetNotSendTenantsStatistics();
        public Task<TenantsStatistics> GetTenantsAnyStatistics(int year, int? month, int? day, int? hour);
        public Task<TenantsStatistics> GetUnproccessedTenantsStatistics();
    }
}

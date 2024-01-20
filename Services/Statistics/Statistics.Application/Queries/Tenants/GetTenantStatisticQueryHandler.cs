using MediatR;
using Statistics.Application.Dto.Out;
using Statistics.Application.Queries.Tenants;
using Statistics.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Application.Queries.Tenants
{
    public class GetTenantStatisticQueryHandler : IRequestHandler<GetTenantStatisticQuery, GetTenantsStatisticResponse>
    {
        private readonly ITenantsStatisticsRepository _tenantsStatisticsRepository;

        public GetTenantStatisticQueryHandler(ITenantsStatisticsRepository tenantsStatisticsRepository)
        {
            _tenantsStatisticsRepository = tenantsStatisticsRepository;
        }

        public async Task<GetTenantsStatisticResponse> Handle(GetTenantStatisticQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var tenantStatistic = await _tenantsStatisticsRepository.GetTenantsAnyStatistics(request.dto.Year, request.dto.Month, request.dto.Day, request.dto.Hour);
                if (tenantStatistic == null)
                {
                    throw new FileNotFoundException("Couldn`t find statistic");
                }
                if (tenantStatistic.IsFullyProcessed != true)
                {
                    throw new FileLoadException("Statistic exists but data is not ready yet");
                }
                return new GetTenantsStatisticResponse() { StatisticsEnd = tenantStatistic.StatisticsEnd.Value, StatisticsStart = tenantStatistic.StatisticsStart.Value, TenantsCreated = tenantStatistic.TenantsCreated, TenantStatisticId = tenantStatistic.TenantStatisticId, TenantsUpdated = tenantStatistic.TenantsUpdated, MostTenantsInRoom = tenantStatistic.MostTenantsInRoom, HighestRent = tenantStatistic.HighestRent };

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

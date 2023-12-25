using MediatR;
using Microsoft.Extensions.Logging;
using Statistics.Application.Commands.Tenants.CreateHourStatistics;
using Statistics.Domain.Entities;
using Statistics.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Application.Commands.Tenants.CreateHourStatistics
{
    public class CreateTenantHourStatisticsCommandHandler : IRequestHandler<CreateTenantHourStatisticsCommand>
    {
        private readonly ITenantsStatisticsRepository _repository;
        private readonly ILogger<CreateTenantHourStatisticsCommandHandler> _logger;
        public CreateTenantHourStatisticsCommandHandler(ITenantsStatisticsRepository repository, ILogger<CreateTenantHourStatisticsCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;

        }
        public async Task Handle(CreateTenantHourStatisticsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _repository.GetTenantsHourStatistics(request.RequestDto.Year, request.RequestDto.Month, request.RequestDto.Day, request.RequestDto.Hour);
                if (data != null)
                {
                    throw new DuplicateNameException("Statistic already exists");
                }
                var statistic = TenantsStatistics.CreateAsHourStatisticsInformations(request.RequestDto.Year, request.RequestDto.Month, request.RequestDto.Day, request.RequestDto.Hour, false);
                await _repository.CreateOrUpdateTenantsStatistics(statistic);
                return;
            }
            catch (DuplicateNameException ex)
            {
                _logger.LogWarning(400, ex, ex.Message);
            }
            catch (Exception ex)
            {

                _logger.LogWarning(500, ex, "CreateTenantHourStatisticsCommand failed");
            }

        }
    }
}

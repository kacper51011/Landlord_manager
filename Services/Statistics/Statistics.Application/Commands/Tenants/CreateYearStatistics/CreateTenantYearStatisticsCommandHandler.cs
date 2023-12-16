using MediatR;
using Microsoft.Extensions.Logging;
using Statistics.Application.Commands.Tenants.CreateYearStatistics;
using Statistics.Domain.Entities;
using Statistics.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Application.Commands.Tenants.CreateYearStatistics
{
    public class CreateTenantYearStatisticsCommandHandler : IRequestHandler<CreateTenantYearStatisticsCommand>
    {
        private readonly ITenantsStatisticsRepository _repository;
        private readonly ILogger<CreateTenantYearStatisticsCommandHandler> _logger;
        public CreateTenantYearStatisticsCommandHandler(ITenantsStatisticsRepository repository, ILogger<CreateTenantYearStatisticsCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;

        }
        public async Task Handle(CreateTenantYearStatisticsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _repository.GetTenantsYearStatistics(request.RequestDto.Year);
                if (data != null)
                {
                    throw new DuplicateNameException("Statistic already exists");
                }
                var statistic = TenantsStatistics.CreateAsYearStatisticsInformations(request.RequestDto.Year);
                await _repository.CreateOrUpdateTenantsStatistics(statistic);
                return;
            }
            catch (DuplicateNameException ex)
            {
                _logger.LogWarning(400, ex, ex.Message);
            }
            catch (Exception ex)
            {

                _logger.LogWarning(500, ex, "CreateTenantYearStatisticsCommand failed");
            }

        }
    }
}

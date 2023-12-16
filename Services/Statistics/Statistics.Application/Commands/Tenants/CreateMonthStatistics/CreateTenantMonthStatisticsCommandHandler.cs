using MediatR;
using Microsoft.Extensions.Logging;
using Statistics.Application.Commands.Tenants.CreateMonthStatistics;
using Statistics.Domain.Entities;
using Statistics.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Application.Commands.Tenants.CreateMonthStatistics
{
    public class CreateTenantMonthStatisticsCommandHandler : IRequestHandler<CreateTenantMonthStatisticsCommand>
    {
        private readonly ITenantsStatisticsRepository _repository;
        private readonly ILogger<CreateTenantMonthStatisticsCommandHandler> _logger;
        public CreateTenantMonthStatisticsCommandHandler(ITenantsStatisticsRepository repository, ILogger<CreateTenantMonthStatisticsCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;

        }
        public async Task Handle(CreateTenantMonthStatisticsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _repository.GetTenantsMonthStatistics(request.RequestDto.Year, request.RequestDto.Month);
                if (data != null)
                {
                    throw new DuplicateNameException("Statistic already exists");
                }
                var statistic = TenantsStatistics.CreateAsMonthStatisticsInformations(request.RequestDto.Year, request.RequestDto.Month);
                await _repository.CreateOrUpdateTenantsStatistics(statistic);
                return;
            }
            catch (DuplicateNameException ex)
            {
                _logger.LogWarning(400, ex, ex.Message);
            }
            catch (Exception ex)
            {

                _logger.LogWarning(500, ex, "CreateTenantMonthStatisticsCommand failed");
            }

        }
    }
}

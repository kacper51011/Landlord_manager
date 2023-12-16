using MediatR;
using Microsoft.Extensions.Logging;
using Statistics.Application.Commands.Tenants.CreateDayStatistics;
using Statistics.Domain.Entities;
using Statistics.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Application.Commands.Tenants.CreateDayStatistics
{
    public class CreateTenantDayStatisticsCommandHandler: IRequestHandler<CreateTenantDayStatisticsCommand>
    {
        private readonly ITenantsStatisticsRepository _repository;
        private readonly ILogger<CreateTenantDayStatisticsCommandHandler> _logger;
        public CreateTenantDayStatisticsCommandHandler(ITenantsStatisticsRepository repository, ILogger<CreateTenantDayStatisticsCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;

        }
        public async Task Handle(CreateTenantDayStatisticsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _repository.GetTenantsDayStatistics(request.RequestDto.Year, request.RequestDto.Month, request.RequestDto.Day);
                if (data != null)
                {
                    throw new DuplicateNameException("Statistic already exists");
                }
                var statistic = TenantsStatistics.CreateAsDayStatisticsInformations(request.RequestDto.Year, request.RequestDto.Month, request.RequestDto.Day);
                await _repository.CreateOrUpdateTenantsStatistics(statistic);
                return;
            }
            catch (DuplicateNameException ex)
            {
                _logger.LogWarning(400, ex, ex.Message);
            }
            catch (Exception ex)
            {

                _logger.LogWarning(500, ex, "CreateTenantDayStatisticsCommand failed");
            }

        }
    }
}

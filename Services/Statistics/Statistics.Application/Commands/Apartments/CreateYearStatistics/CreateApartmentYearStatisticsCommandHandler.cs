using MediatR;
using Microsoft.Extensions.Logging;
using Statistics.Application.Commands.Apartments.CreateYearStatistics;
using Statistics.Domain.Entities;
using Statistics.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Application.Commands.Apartments.CreateYearStatistics
{
    public class CreateApartmentYearStatisticsCommandHandler : IRequestHandler<CreateApartmentYearStatisticsCommand>
    {
        private readonly IApartmentsStatisticsRepository _repository;
        private readonly ILogger<CreateApartmentYearStatisticsCommandHandler> _logger;
        public CreateApartmentYearStatisticsCommandHandler(IApartmentsStatisticsRepository repository, ILogger<CreateApartmentYearStatisticsCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;

        }
        public async Task Handle(CreateApartmentYearStatisticsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _repository.GetApartmentYearStatistics(request.RequestDto.Year);
                if (data != null)
                {
                    throw new DuplicateNameException("Statistic already exists");
                }
                var statistic = ApartmentsStatistics.CreateAsYearStatisticsInformations(request.RequestDto.Year, false);
                return;


            }
            catch (DuplicateNameException ex)
            {
                _logger.LogWarning(400, ex, ex.Message);
            }
            catch (Exception ex)
            {

                _logger.LogWarning(500, ex, "CreateApartmentYearStatisticsCommand failed");
            }

        }
    }
}

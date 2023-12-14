using MediatR;
using Microsoft.Extensions.Logging;
using Statistics.Application.Commands.Apartments.CreateMonthStatistics;
using Statistics.Domain.Entities;
using Statistics.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Application.Commands.Apartments.CreateMonthStatistics
{
    public class CreateApartmentMonthStatisticsCommandHandler : IRequestHandler<CreateApartmentMonthStatisticsCommand>
    {
        private readonly IApartmentsStatisticsRepository _repository;
        private readonly ILogger<CreateApartmentMonthStatisticsCommandHandler> _logger;
        public CreateApartmentMonthStatisticsCommandHandler(IApartmentsStatisticsRepository repository, ILogger<CreateApartmentMonthStatisticsCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;

        }
        public async Task Handle(CreateApartmentMonthStatisticsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _repository.GetApartmentMonthStatistics(request.RequestDto.Year, request.RequestDto.Month);
                if (data != null)
                {
                    throw new DuplicateNameException("Statistic already exists");
                }
                var statistic = ApartmentsStatistics.CreateAsMonthStatisticsInformations(request.RequestDto.Year, request.RequestDto.Month);
                return;


            }
            catch (DuplicateNameException ex)
            {
                _logger.LogWarning(400, ex, ex.Message);
            }
            catch (Exception ex)
            {

                _logger.LogWarning(500, ex, "CreateApartmentMonthStatisticsCommand failed");
            }

        }
    }
}

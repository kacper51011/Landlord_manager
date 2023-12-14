using MediatR;
using Microsoft.Extensions.Logging;
using Statistics.Domain.Entities;
using Statistics.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Application.Commands.Apartments.CreateHourStatistics
{
    public class CreateApartmentHourStatisticsCommandHandler : IRequestHandler<CreateApartmentHourStatisticsCommand>
    {
        private readonly IApartmentsStatisticsRepository _repository;
        private readonly ILogger<CreateApartmentHourStatisticsCommandHandler> _logger;
        public CreateApartmentHourStatisticsCommandHandler(IApartmentsStatisticsRepository repository, ILogger<CreateApartmentHourStatisticsCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
            
        }
        public async Task Handle(CreateApartmentHourStatisticsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _repository.GetApartmentHourStatistics(request.RequestDto.Year, request.RequestDto.Month, request.RequestDto.Day, request.RequestDto.Hour);
                if (data != null)
                {
                        throw new DuplicateNameException("Statistic already exists");
                }
                var statistic = ApartmentsStatistics.CreateAsHourStatisticsInformations(request.RequestDto.Year, request.RequestDto.Month, request.RequestDto.Day, request.RequestDto.Hour);
                return;


            }
            catch (DuplicateNameException ex)
            {
                _logger.LogWarning(400, ex, ex.Message);
            }
            catch (Exception ex)
            {

                _logger.LogWarning(500, ex, "CreateApartmentHourStatisticsCommand failed");
            }

        }
    }
}

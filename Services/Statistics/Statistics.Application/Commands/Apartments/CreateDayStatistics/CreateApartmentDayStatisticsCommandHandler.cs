using MediatR;
using Microsoft.Extensions.Logging;
using Statistics.Application.Commands.Rooms.CreateDayStatistics;
using Statistics.Domain.Entities;
using Statistics.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Application.Commands.Apartments.CreateDayStatistics
{
    public class CreateApartmentDayStatisticsCommandHandler : IRequestHandler<CreateApartmentDayStatisticsCommand>
    {
        private readonly IApartmentsStatisticsRepository _repository;
        private readonly ILogger<CreateApartmentDayStatisticsCommandHandler> _logger;
        public CreateApartmentDayStatisticsCommandHandler(IApartmentsStatisticsRepository repository, ILogger<CreateApartmentDayStatisticsCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;

        }
        public async Task Handle(CreateApartmentDayStatisticsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _repository.GetApartmentDayStatistics(request.RequestDto.Year, request.RequestDto.Month, request.RequestDto.Day);
                if (data != null)
                {
                    throw new DuplicateNameException("Statistic already exists");
                }
                var statistic = ApartmentsStatistics.CreateAsDayStatisticsInformations(request.RequestDto.Year, request.RequestDto.Month, request.RequestDto.Day, false);
                await _repository.CreateOrUpdateApartmentStatistics(statistic);
                return;
            }
            catch (DuplicateNameException ex)
            {
                _logger.LogWarning(400, ex, ex.Message);
            }
            catch (Exception ex)
            {

                _logger.LogWarning(500, ex, "CreateApartmentDayStatisticsCommand failed");
            }

        }
    }
}

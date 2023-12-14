using MediatR;
using Microsoft.Extensions.Logging;
using Statistics.Application.Commands.Rooms.CreateYearStatistics;
using Statistics.Domain.Entities;
using Statistics.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Application.Commands.Rooms.CreateYearStatistics
{
    public class CreateRoomYearStatisticsCommandHandler : IRequestHandler<CreateRoomYearStatisticsCommand>
    {
        private readonly IRoomsStatisticsRepository _repository;
        private readonly ILogger<CreateRoomYearStatisticsCommandHandler> _logger;
        public CreateRoomYearStatisticsCommandHandler(IRoomsStatisticsRepository repository, ILogger<CreateRoomYearStatisticsCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;

        }
        public async Task Handle(CreateRoomYearStatisticsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _repository.GetRoomsYearStatistics(request.RequestDto.Year);
                if (data != null)
                {
                    throw new DuplicateNameException("Statistic already exists");

                }
                var statistic = RoomsStatistics.CreateAsYearStatisticsInformations(request.RequestDto.Year);
                await _repository.CreateOrUpdateRoomsStatistics(statistic);
                return;
            }
            catch (DuplicateNameException ex)
            {
                _logger.LogWarning(400, ex, ex.Message);
            }
            catch (Exception ex)
            {

                _logger.LogWarning(500, ex, "CreateRoomYearStatisticsCommandHandler failed");
            }

        }
    }
}

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

namespace Statistics.Application.Commands.Rooms.CreateDayStatistics
{
    public class CreateRoomDayStatisticsCommandHandler : IRequestHandler<CreateRoomDayStatisticsCommand>
    {
        private readonly IRoomsStatisticsRepository _repository;
        private readonly ILogger<CreateRoomDayStatisticsCommandHandler> _logger;
        public CreateRoomDayStatisticsCommandHandler(IRoomsStatisticsRepository repository, ILogger<CreateRoomDayStatisticsCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;

        }
        public async Task Handle(CreateRoomDayStatisticsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _repository.GetRoomsDayStatistics(request.RequestDto.Year, request.RequestDto.Month, request.RequestDto.Day);
                if (data != null)
                {
                    throw new DuplicateNameException("Statistic already exists");
                }
                var statistic = RoomsStatistics.CreateAsDayStatisticsInformations(request.RequestDto.Year, request.RequestDto.Month, request.RequestDto.Day);
                await _repository.CreateOrUpdateRoomsStatistics(statistic);
                return;
            }
            catch (DuplicateNameException ex)
            {
                _logger.LogWarning(400, ex, ex.Message);
            }
            catch (Exception ex)
            {

                _logger.LogWarning(500, ex, "CreateRoomDayStatisticsCommandHandler failed");
            }

        }
    }
}

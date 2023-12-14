using MediatR;
using Microsoft.Extensions.Logging;
using Statistics.Application.Commands.Rooms.CreateHourStatistics;
using Statistics.Application.Dto;
using Statistics.Domain.Entities;
using Statistics.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Application.Commands.Rooms.CreateHourStatistics
{
    public class CreateRoomHourStatisticsCommandHandler : IRequestHandler<CreateRoomHourStatisticsCommand>
    {
        private readonly IRoomsStatisticsRepository _repository;
        private readonly ILogger<CreateRoomHourStatisticsCommandHandler> _logger;
        public CreateRoomHourStatisticsCommandHandler(IRoomsStatisticsRepository repository, ILogger<CreateRoomHourStatisticsCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;

        }
        public async Task Handle(CreateRoomHourStatisticsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _repository.GetRoomsHourStatistics(request.RequestDto.Year, request.RequestDto.Month, request.RequestDto.Day, request.RequestDto.Hour);
                if (data != null)
                {
                    throw new DuplicateNameException("Statistic already exists");
                    
                }
                var statistic = RoomsStatistics.CreateAsHourStatisticsInformations(request.RequestDto.Year, request.RequestDto.Month, request.RequestDto.Day, request.RequestDto.Hour);
                await _repository.CreateOrUpdateRoomsStatistics(statistic);
                return;
            }
            catch(DuplicateNameException ex)
            {
                _logger.LogWarning(400, ex, ex.Message);
            }
            catch (Exception ex)
            {

                _logger.LogWarning(500, ex, "CreateRoomHourStatisticsCommandHandler failed");
            }

        }
    }
}

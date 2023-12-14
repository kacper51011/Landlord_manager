using MediatR;
using Microsoft.Extensions.Logging;
using Statistics.Application.Commands.Rooms.CreateMonthStatistics;
using Statistics.Domain.Entities;
using Statistics.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Application.Commands.Rooms.CreateMonthStatistics
{
    public class CreateRoomMonthStatisticsCommandHandler : IRequestHandler<CreateRoomMonthStatisticsCommand>
    {
        private readonly IRoomsStatisticsRepository _repository;
        private readonly ILogger<CreateRoomMonthStatisticsCommandHandler> _logger;
        public CreateRoomMonthStatisticsCommandHandler(IRoomsStatisticsRepository repository, ILogger<CreateRoomMonthStatisticsCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;

        }
        public async Task Handle(CreateRoomMonthStatisticsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _repository.GetRoomsMonthStatistics(request.RequestDto.Year, request.RequestDto.Month);
                if (data != null)
                {
                    throw new DuplicateNameException("Statistic already exists");

                }
                var statistic = RoomsStatistics.CreateAsMonthStatisticsInformations(request.RequestDto.Year, request.RequestDto.Month);
                await _repository.CreateOrUpdateRoomsStatistics(statistic);
                return;
            }
            catch (DuplicateNameException ex)
            {
                _logger.LogWarning(400, ex, ex.Message);
            }
            catch (Exception ex)
            {

                _logger.LogWarning(500, ex, "CreateRoomMonthStatisticsCommandHandler failed");
            }

        }
    }
}

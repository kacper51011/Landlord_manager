using Contracts.StatisticsMessages;
using MassTransit;
using Microsoft.Extensions.Logging;
using Statistics.Application.Consumers.Rooms;
using Statistics.Domain.Entities;
using Statistics.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Application.Consumers.Rooms
{
    public class RoomsYearStatisticsMessageConsumer: IConsumer<StatisticYearMessage>
    {
        private readonly ILogger<RoomsYearStatisticsMessageConsumer> _logger;
        private readonly IRoomsStatisticsRepository _RoomsStatisticsRepository;
        public RoomsYearStatisticsMessageConsumer(ILogger<RoomsYearStatisticsMessageConsumer> logger, IRoomsStatisticsRepository RoomsStatisticsRepository)
        {
            _logger = logger;
            _RoomsStatisticsRepository = RoomsStatisticsRepository;

        }
        public async Task Consume(ConsumeContext<StatisticYearMessage> context)
        {
            try
            {
                var response = await _RoomsStatisticsRepository.GetRoomsYearStatistics(context.Message.Year);
                if (response != null)
                {
                    _logger.LogInformation($"Year Room statistic for date {response.StatisticsStart} was already created");
                    return;
                }
                RoomsStatistics RoomStatistic = RoomsStatistics.CreateAsYearStatisticsInformations(context.Message.Year);
                await _RoomsStatisticsRepository.CreateOrUpdateRoomsStatistics(RoomStatistic);
                _logger.LogInformation($"Year Room statistic created with Id {RoomStatistic.RoomsStatisticsId}");
                return;
            }
            catch (Exception)
            {
                _logger.LogError($"Something went wrong in RoomsYearStatisticsMessageConsumer");
                throw;
            }
        }
    }
}

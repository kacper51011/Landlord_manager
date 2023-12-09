using Contracts.StatisticsMessages;
using MassTransit;
using Microsoft.Extensions.Logging;
using Statistics.Domain.Entities;
using Statistics.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Application.Consumers.Rooms
{
    public class RoomsHourStatisticsMessageConsumer: IConsumer<StatisticHourMessage>
    {
        private readonly ILogger<RoomsHourStatisticsMessageConsumer> _logger;
        private readonly IRoomsStatisticsRepository _roomsStatisticsRepository;
        public RoomsHourStatisticsMessageConsumer(ILogger<RoomsHourStatisticsMessageConsumer> logger, IRoomsStatisticsRepository roomsStatisticsRepository)
        {
            _logger = logger;
            _roomsStatisticsRepository = roomsStatisticsRepository;

        }
        public async Task Consume(ConsumeContext<StatisticHourMessage> context)
        {
            try
            {
                var response = await _roomsStatisticsRepository.GetRoomsHourStatistics(context.Message.Year, context.Message.Month, context.Message.Day, context.Message.Hour);
                if (response != null)
                {
                    _logger.LogInformation($"Hour apartment statistic for date {response.StatisticsStart} was already created");
                    return;
                }
                RoomsStatistics apartmentStatistic = RoomsStatistics.CreateAsHourStatisticsInformations(context.Message.Year, context.Message.Month, context.Message.Day, context.Message.Hour);
                await _roomsStatisticsRepository.CreateOrUpdateRoomsStatistics(apartmentStatistic);
                _logger.LogInformation($"Hour apartment statistic created with Id {apartmentStatistic.RoomsStatisticsId}");
                return;
            }
            catch (Exception)
            {
                _logger.LogError($"Something went wrong in RoomsHourStatisticsMessageConsumer");
                throw;
            }
        }
    }
}

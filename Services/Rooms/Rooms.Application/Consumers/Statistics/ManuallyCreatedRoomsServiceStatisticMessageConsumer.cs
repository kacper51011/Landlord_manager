using Contracts.StatisticsMessages.Rooms;
using MassTransit;
using Microsoft.Extensions.Logging;
using Rooms.Domain;
using Rooms.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rooms.Application.Consumers.Statistics
{
    public class ManuallyCreatedRoomsServiceStatisticMessageConsumer : IConsumer<ManuallyCreatedRoomStatisticsMessage>
    {
        private readonly IRoomsStatisticsRepository _statisticsRepository;
        private readonly ILogger<ManuallyCreatedRoomsServiceStatisticMessageConsumer> _logger;
        public ManuallyCreatedRoomsServiceStatisticMessageConsumer(IRoomsStatisticsRepository statisticsRepository, ILogger<ManuallyCreatedRoomsServiceStatisticMessageConsumer> logger)
        {
            _statisticsRepository = statisticsRepository;
            _logger = logger;

        }
        public async Task Consume(ConsumeContext<ManuallyCreatedRoomStatisticsMessage> context)
        {
            try
            {
                var statistic = await _statisticsRepository.GetRoomAnyStatistics(context.Message.Year, context.Message.Month, context.Message.Day, context.Message.Hour);
                if (statistic != null)
                {
                    //If we did get this message while our statistic is not null, it means that our statistic could be lost
                    _logger.LogInformation($"Room Statistic already created but requested by StatisticsService");
                    statistic.SetIsSentToStatisticsService(false);
                    await _statisticsRepository.CreateOrUpdateRoomStatistics(statistic);
                    return;
                }
                RoomsStatistics roomsStatistics;
                //a bit other strategy compared to how i handled types of statistics, but i think it was better to handle it separately in cqrs

                //create as hour
                if (context.Message.Hour.HasValue && context.Message.Month.HasValue && context.Message.Day.HasValue)
                {
                    roomsStatistics = RoomsStatistics.CreateAsHourStatisticsInformations(context.Message.Year, context.Message.Month.Value, context.Message.Day.Value, context.Message.Hour.Value);
                }

                //create as day
                else if (context.Message.Hour == null && context.Message.Month.HasValue && context.Message.Day.HasValue)
                {
                    roomsStatistics = RoomsStatistics.CreateAsDayStatisticsInformations(context.Message.Year, context.Message.Month.Value, context.Message.Day.Value);
                }

                //create as month
                else if (context.Message.Day == null && context.Message.Hour == null && context.Message.Month.HasValue)
                {
                    roomsStatistics = RoomsStatistics.CreateAsMonthStatisticsInformations(context.Message.Year, context.Message.Month.Value);
                }

                //create as year
                else
                {
                    roomsStatistics = RoomsStatistics.CreateAsYearStatisticsInformations(context.Message.Year);
                }

                await _statisticsRepository.CreateOrUpdateRoomStatistics(roomsStatistics);
            }
            catch (Exception)
            {
                _logger.LogWarning("Something went wrong in ManuallyCreatedStatisticMessageConsumer in apartments service");
                throw;
            }

        }
    }
}

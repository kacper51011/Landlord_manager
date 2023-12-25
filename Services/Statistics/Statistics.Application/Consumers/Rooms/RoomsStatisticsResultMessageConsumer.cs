using Contracts.StatisticsMessages.Rooms;
using Contracts.StatisticsMessages.Rooms;
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
    public class RoomsStatisticsResultMessageConsumer : IConsumer<RoomsStatisticsResultMessage>
    {
        private readonly ILogger<RoomsStatisticsResultMessageConsumer> _logger;
        private readonly IRoomsStatisticsRepository _roomsStatisticsRepository;
        public RoomsStatisticsResultMessageConsumer(ILogger<RoomsStatisticsResultMessageConsumer> logger, IRoomsStatisticsRepository roomsStatisticsRepository)
        {
            _logger = logger;
            _roomsStatisticsRepository = roomsStatisticsRepository;

        }
        public async Task Consume(ConsumeContext<RoomsStatisticsResultMessage> context)
        {
            try
            {
                var statistic = await _roomsStatisticsRepository.GetRoomsAnyStatistics(context.Message.Year, context.Message.Month, context.Message.Day, context.Message.Hour);
                if (statistic == null)
                {
                    RoomsStatistics roomsStatistics;
                    //a bit other strategy compared to how i handled types of statistics, but i think it was better to handle it separately in cqrs

                    //create as hour
                    if (context.Message.Hour.HasValue && context.Message.Month.HasValue && context.Message.Day.HasValue)
                    {
                        roomsStatistics = RoomsStatistics.CreateAsHourStatisticsInformations(context.Message.Year, context.Message.Month.Value, context.Message.Day.Value, context.Message.Hour.Value, true);
                    }

                    //create as day
                    if (context.Message.Hour == null && context.Message.Month.HasValue && context.Message.Day.HasValue)
                    {
                        roomsStatistics = RoomsStatistics.CreateAsDayStatisticsInformations(context.Message.Year, context.Message.Month.Value, context.Message.Day.Value, true);
                    }

                    //create as month
                    if (context.Message.Day == null && context.Message.Hour == null && context.Message.Month.HasValue)
                    {
                        roomsStatistics = RoomsStatistics.CreateAsMonthStatisticsInformations(context.Message.Year, context.Message.Month.Value, true);
                    }

                    //create as year
                    else
                    {
                        roomsStatistics = RoomsStatistics.CreateAsYearStatisticsInformations(context.Message.Year, true);
                    }

                    roomsStatistics.SetStatistics(context.Message.RoomsCreated, context.Message.RoomsUpdated, context.Message.BiggestCreatedRoomSize, context.Message.MostRoomsInApartment);
                    await _roomsStatisticsRepository.CreateOrUpdateRoomsStatistics(statistic);
                    _logger.LogInformation($"created new statistic from consumer, statistic with id {statistic.RoomsStatisticsId}");
                    return;

                }


                statistic.SetStatistics(context.Message.RoomsCreated, context.Message.RoomsUpdated, context.Message.BiggestCreatedRoomSize, context.Message.MostRoomsInApartment);
                await _roomsStatisticsRepository.CreateOrUpdateRoomsStatistics(statistic);
                _logger.LogInformation($"updated statistic with id {statistic.RoomsStatisticsId}");
            }

            catch (Exception ex)
            {
                _logger.LogError($"Updating with result failed");

                throw ex;
            }

        }
    }
}

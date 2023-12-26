using Apartments.Domain;
using Apartments.Domain.Interfaces;
using Contracts.StatisticsMessages.Apartments;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Apartments.Application.Consumers.Statistics
{
    public class ManuallyCreatedStatisticMessageConsumer : IConsumer<ManuallyCreatedApartmentStatisticsMessage>
    {
        private readonly IApartmentsStatisticsRepository _statisticsRepository;
        private readonly ILogger<ManuallyCreatedStatisticMessageConsumer> _logger;
        public ManuallyCreatedStatisticMessageConsumer(IApartmentsStatisticsRepository statisticsRepository, ILogger<ManuallyCreatedStatisticMessageConsumer> logger)
        {
            _statisticsRepository = statisticsRepository;
            _logger = logger;

        }
        public async Task Consume(ConsumeContext<ManuallyCreatedApartmentStatisticsMessage> context)
        {
            try
            {
                var statistic = await _statisticsRepository.GetApartmentAnyStatistics(context.Message.Year, context.Message.Month, context.Message.Day, context.Message.Hour);
                if (statistic != null)
                {
                    _logger.LogInformation($"Apartment Statistic already created");
                    return;
                }
                ApartmentsStatistics roomsStatistics;
                //a bit other strategy compared to how i handled types of statistics, but i think it was better to handle it separately in cqrs

                //create as hour
                if (context.Message.Hour.HasValue && context.Message.Month.HasValue && context.Message.Day.HasValue)
                {
                    roomsStatistics = ApartmentsStatistics.CreateAsHourStatisticsInformations(context.Message.Year, context.Message.Month.Value, context.Message.Day.Value, context.Message.Hour.Value);
                }

                //create as day
                 else if (context.Message.Hour == null && context.Message.Month.HasValue && context.Message.Day.HasValue)
                {
                    roomsStatistics = ApartmentsStatistics.CreateAsDayStatisticsInformations(context.Message.Year, context.Message.Month.Value, context.Message.Day.Value);
                }

                //create as month
                else if (context.Message.Day == null && context.Message.Hour == null && context.Message.Month.HasValue)
                {
                    roomsStatistics = ApartmentsStatistics.CreateAsMonthStatisticsInformations(context.Message.Year, context.Message.Month.Value);
                }

                //create as year
                else
                {
                    roomsStatistics = ApartmentsStatistics.CreateAsYearStatisticsInformations(context.Message.Year);
                }

                await _statisticsRepository.CreateOrUpdateApartmentStatistics(roomsStatistics);
            }
            catch (Exception)
            {
                _logger.LogWarning("Something went wrong in ManuallyCreatedStatisticMessageConsumer in apartments service");
                throw;
            }

        }
    }
}

using Apartments.Domain.Interfaces;
using Contracts.StatisticsMessages.Apartments;
using MassTransit;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Apartments.Application.Workers
{
    public class SendStatisticsBackgroundJob : IJob
    {
        private readonly IApartmentsStatisticsRepository _apartmentsStatisticsRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<SendStatisticsBackgroundJob> _logger;

        public SendStatisticsBackgroundJob(IApartmentsStatisticsRepository apartmentsStatisticsRepository, IPublishEndpoint publishEndpoint, ILogger<SendStatisticsBackgroundJob> logger)
        {
            _apartmentsStatisticsRepository = apartmentsStatisticsRepository;
            _publishEndpoint = publishEndpoint;
            _logger = logger;

        }
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                //atomically finds and updates the statistic
                var notSendStatistic = await _apartmentsStatisticsRepository.GetNotSendApartmentsStatistics();
                if (notSendStatistic == null)
                {
                    _logger.LogInformation("No statistic to send in apartments service");
                    return;
                }
                var statisticResultMessage = new ApartmentsStatisticsResultMessage
                {
                    ApartmentsCreated = notSendStatistic.ApartmentsCreated,
                    ApartmentsUpdated = notSendStatistic.ApartmentsUpdated,
                    MostApartmentsOwnedByUser = notSendStatistic.MostApartmentsOwnedByUser,
                    Year = notSendStatistic.Year.Value,
                    Month = notSendStatistic.Month.Value,
                    Day = notSendStatistic.Day.Value,
                    Hour = notSendStatistic.Hour.Value
                };
                await _publishEndpoint.Publish(statisticResultMessage);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Something went wrong in SendStatisticsBackgroundJob in apartmentsService");
                throw;
            }




        }
    }
}

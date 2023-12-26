using Contracts.StatisticsMessages.Apartments;
using Contracts.StatisticsMessages.Rooms;
using MassTransit;
using Microsoft.Extensions.Logging;
using Quartz;
using Statistics.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Application.Workers.SendingJobs
{
    public class SendToRoomsJob: IJob
    {
        private readonly IPublishEndpoint _publisher;
        private readonly IRoomsStatisticsRepository _repository;
        private readonly ILogger<SendToRoomsJob> _logger;
        public SendToRoomsJob(IRoomsStatisticsRepository repository, ILogger<SendToRoomsJob> logger, IPublishEndpoint publisher)
        {
            _repository = repository;
            _logger = logger;
            _publisher = publisher;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var notSentStatistic = await _repository.GetNotSentRoomStatistics();
                if (notSentStatistic == null)
                {
                    _logger.LogInformation("No Room statistic to send");
                    return;
                }
                await _publisher.Publish(new ManuallyCreatedRoomStatisticsMessage { Hour = notSentStatistic.Hour.Value, Day= notSentStatistic.Day.Value, Month = notSentStatistic.Month.Value, Year = notSentStatistic.Year.Value});
                notSentStatistic.SetIsSent(true);
                await _repository.CreateOrUpdateRoomsStatistics(notSentStatistic);
                _logger.LogInformation($"Sent ManuallyCreatedRoomStatisticsMessage for statistic with Id {notSentStatistic.RoomsStatisticsId}");

            }
            catch (Exception)
            {
                _logger.LogWarning($"Something went wrong in ManuallyCreatedRoomStatisticsMessage");
                throw;
            }
        }
    }
}

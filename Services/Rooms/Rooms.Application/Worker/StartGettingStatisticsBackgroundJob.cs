using Contracts.RoomsService.RoomsStatistics;
using MassTransit;
using Microsoft.Extensions.Logging;
using Quartz;
using Rooms.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rooms.Application.Worker
{
    public class StartGettingStatisticsBackgroundJob: IJob
    {
        private readonly IPublishEndpoint _publisher;
        private readonly IRoomsStatisticsRepository _repository;
        private readonly ILogger<StartGettingStatisticsBackgroundJob> _logger;
        public StartGettingStatisticsBackgroundJob(IRoomsStatisticsRepository repository, ILogger<StartGettingStatisticsBackgroundJob> logger, IPublishEndpoint publisher)
        {
            _publisher = publisher;
            _repository = repository;
            _logger = logger;

        }
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                // gets unprocessed statistics (only one currently)
                var emptyStatistics = await _repository.GetUnproccessedRoomStatistics();
                if (emptyStatistics == null)
                {
                    _logger.LogInformation("Can`t find any unprocessed statistics in RoomsStatistics");
                    return;
                }
                await _publisher.Publish(new RoomStatisticToProcessMessage { RoomStatisticId = emptyStatistics.RoomsStatisticsId });
                _logger.LogInformation("Empty statistic sent to process in Rooms Service");
                return;


            }
            catch (Exception ex)
            {
                _logger.LogWarning("Something went wrong in StartGettingStatisticsBackgroundJob in rooms service");
                throw;
            }
        }
    }
}

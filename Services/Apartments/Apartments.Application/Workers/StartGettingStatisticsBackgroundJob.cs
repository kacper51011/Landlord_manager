using Amazon.Runtime.Internal.Util;
using Apartments.Domain.Interfaces;
using Contracts.ApartmentsService.ApartmentsStatistics;
using MassTransit;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apartments.Application.Workers
{
    public class StartGettingStatisticsBackgroundJob : IJob
    {
        private readonly IPublishEndpoint _publisher;
        private readonly IApartmentsStatisticsRepository _repository;
        private readonly ILogger<StartGettingStatisticsBackgroundJob> _logger;
        public StartGettingStatisticsBackgroundJob(IApartmentsStatisticsRepository repository, ILogger<StartGettingStatisticsBackgroundJob> logger, IPublishEndpoint publisher)
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
                var emptyStatistics = await _repository.GetUnproccessedApartmentStatistics();
                if (emptyStatistics == null)
                {
                    _logger.LogInformation("Can`t find any unprocessed statistics in ApartmentStatistics");
                    return;
                }
                await _publisher.Publish(new ApartmentStatisticToProcessMessage { ApartmentStatisticId = emptyStatistics.ApartmentsStatisticsId });
                _logger.LogInformation("Empty statistic sent to queue");
                return;


            }
            catch (Exception ex)
            {
                _logger.LogWarning("Something went wrong in StartGettingStatisticsBackgroundJob");
                throw;
            }
        }
    }
}

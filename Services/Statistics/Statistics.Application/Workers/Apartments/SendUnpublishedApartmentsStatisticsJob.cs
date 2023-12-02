using Contracts.StatisticsMessages.Apartments;
using MassTransit;
using Microsoft.Extensions.Logging;
using Quartz;
using Statistics.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Application.Workers.Apartments
{
    public class SendUnpublishedJob : IJob
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<SendUnpublishedJob> _logger;
        private readonly IApartmentsStatisticsRepository _apartmentsStatisticsRepository;
        public SendUnpublishedJob(IPublishEndpoint publishEndpoint ,ILogger<SendUnpublishedJob> logger, IApartmentsStatisticsRepository apartmentsStatisticsRepository)
        {
            _publishEndpoint = publishEndpoint;
            _logger = logger;
            _apartmentsStatisticsRepository = apartmentsStatisticsRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var unprocessedApartmentsStatistics = await _apartmentsStatisticsRepository.GetNotProcessedStatistics();

                if(unprocessedApartmentsStatistics == null)
                {
                    _logger.LogInformation("No apartmentsStatistics to publish, every is processed already");
                    return;
                }
                foreach(var statistic in  unprocessedApartmentsStatistics)
                {
                    await _publishEndpoint.Publish(new ApartmentsStatisticsMessage()
                    {
                        StatisticsId= statistic.ApartmentsStatisticsId,
                        StatisticsStart = statistic.StatisticsStart,
                        StatisticsEnd = statistic.StatisticsEnd
                    });
                    _logger.LogInformation($"ApartmentsStatistics with Id {statistic.ApartmentsStatisticsId}");
                }

            }
            catch (Exception)
            {

                throw;
            }

            
        }
    }
}

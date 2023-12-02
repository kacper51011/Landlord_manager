using Contracts.StatisticsMessages;
using MassTransit;
using Microsoft.Extensions.Logging;
using Quartz;
using Statistics.Domain.Entities;
using Statistics.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Application.Workers.Apartments
{
    public class ApartmentsStatisticsJob : IJob
    {
        //private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<ApartmentsStatisticsJob> _logger;
        private readonly IApartmentsStatisticsRepository _apartmentsStatisticsRepository;
        public ApartmentsStatisticsJob(ILogger<ApartmentsStatisticsJob> logger, IApartmentsStatisticsRepository apartmentsStatisticsRepository)
        {
            //_publishEndpoint = publishEndpoint;
            _logger = logger;
            _apartmentsStatisticsRepository = apartmentsStatisticsRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var apartmentStatistics = ApartmentsStatistics.CreateEmpty(DateTime.UtcNow.AddMinutes(-50), DateTime.UtcNow.AddMinutes(10), "hour");
                _logger.LogInformation($"creating the statistics for Apartments between ");
                await _apartmentsStatisticsRepository.CreateOrUpdateApartmentStatistics(apartmentStatistics);


            }
            catch (Exception)
            {

                throw;
            }



            //The plan is to start 50 minutes after hour start, then take a scope of one hour as a statistic get in the services
            // await _publishEndpoint.Publish(new RequestStatisticsMessage { TimeStart = DateTime.UtcNow.AddHours(-1), TimeEnd = DateTime.UtcNow });
        }
    }
}
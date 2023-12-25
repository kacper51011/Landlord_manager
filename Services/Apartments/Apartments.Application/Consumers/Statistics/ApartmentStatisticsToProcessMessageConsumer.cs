using Amazon.Runtime.Internal.Util;
using Apartments.Domain.Interfaces;
using Contracts.ApartmentsService.ApartmentsStatistics;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apartments.Application.Consumers.Statistics
{
    public class ApartmentStatisticsToProcessMessageConsumer : IConsumer<ApartmentStatisticToProcessMessage>
    {
        private readonly IApartmentsRepository _apartmentsReposistory;
        private readonly IApartmentsStatisticsRepository _statisticsRepository;
        private readonly ILogger<ApartmentStatisticsToProcessMessageConsumer> _logger;
        public ApartmentStatisticsToProcessMessageConsumer(IApartmentsRepository apartmentsRepository, IApartmentsStatisticsRepository statisticsRepository, ILogger<ApartmentStatisticsToProcessMessageConsumer> logger)
        {
            _apartmentsReposistory = apartmentsRepository;
            _statisticsRepository = statisticsRepository;
            _logger = logger;
            
        }
        public async Task Consume(ConsumeContext<ApartmentStatisticToProcessMessage> context)
        {
            try
            {
                var statisticToProcess = await _statisticsRepository.GetApartmentStatisticsById(context.Message.ApartmentStatisticId);
                if (statisticToProcess == null)
                {
                    throw new ArgumentNullException();
                }
                var createdApartmentsCount = await _apartmentsReposistory.GetCreatedApartmentsCount(statisticToProcess.StatisticsStart.Value, statisticToProcess.StatisticsEnd.Value);
                var updatedApartmentsCount = await _apartmentsReposistory.GetUpdatedApartmentsCount(statisticToProcess.StatisticsStart.Value, statisticToProcess.StatisticsEnd.Value);
                var mostApartmentsOwnedByUser = await _apartmentsReposistory.GetMostApartmentsOwnedByOneUserCount(statisticToProcess.StatisticsEnd.Value);

                statisticToProcess.SetInformations(createdApartmentsCount, updatedApartmentsCount, mostApartmentsOwnedByUser);
                await _statisticsRepository.CreateOrUpdateApartmentStatistics(statisticToProcess);

            }
            catch (Exception)
            {
                _logger.LogWarning("Something went wrong in ApartmentsStatisticToProcessMessageConsuemr");
                throw;
            }
        }
    }
}

using Contracts.TenantsService.TenantsStatistics;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenants.Domain.Interfaces;

namespace Tenants.Application.Consumers.Statistics
{
    public class TenantsServiceStatisticsToProcessMessageConsumer : IConsumer<TenantStatisticToProcessMessage>
    {
        private readonly ITenantsRepository _tenantsReposistory;
        private readonly ITenantsStatisticsRepository _statisticsRepository;
        private readonly ILogger<TenantsServiceStatisticsToProcessMessageConsumer> _logger;
        public TenantsServiceStatisticsToProcessMessageConsumer(ITenantsRepository tenantsRepository, ITenantsStatisticsRepository statisticsRepository, ILogger<TenantsServiceStatisticsToProcessMessageConsumer> logger)
        {
            _tenantsReposistory = tenantsRepository;
            _statisticsRepository = statisticsRepository;
            _logger = logger;

        }
        public async Task Consume(ConsumeContext<TenantStatisticToProcessMessage> context)
        {
            try
            {
                var statisticToProcess = await _statisticsRepository.GetTenantsStatisticsById(context.Message.TenantStatisticId);
                if (statisticToProcess == null)
                {
                    throw new ArgumentNullException();
                }
                var createdTenantsCount = await _tenantsReposistory.GetCreatedTenantsCount(statisticToProcess.StatisticsStart.Value, statisticToProcess.StatisticsEnd.Value);
                var updatedTenantsCount = await _tenantsReposistory.GetUpdatedTenantsCount(statisticToProcess.StatisticsStart.Value, statisticToProcess.StatisticsEnd.Value);
                var mostTenantsInRoom = await _tenantsReposistory.GetMostTenantsInOneRoomCount(statisticToProcess.StatisticsEnd.Value);
                var highestRent = await _tenantsReposistory.GetHighestRentValue(statisticToProcess.StatisticsStart.Value, statisticToProcess.StatisticsEnd.Value);

                statisticToProcess.SetStatistics(createdTenantsCount, updatedTenantsCount, highestRent, mostTenantsInRoom);
                await _statisticsRepository.CreateOrUpdateTenantsStatistics(statisticToProcess);

            }
            catch (Exception)
            {
                _logger.LogWarning("Something went wrong in TenantsStatisticToProcessMessageConsuemr");
                throw;
            }
        }
    }
}

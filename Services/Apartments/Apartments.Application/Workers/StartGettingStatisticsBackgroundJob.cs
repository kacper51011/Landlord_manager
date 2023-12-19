using Amazon.Runtime.Internal.Util;
using Apartments.Domain.Interfaces;
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
        private readonly IApartmentsStatisticsRepository _repository;
        private readonly ILogger<StartGettingStatisticsBackgroundJob> _logger;
        public StartGettingStatisticsBackgroundJob(IApartmentsStatisticsRepository repository, ILogger<StartGettingStatisticsBackgroundJob> logger)
        {
            _repository = repository;
            _logger = logger;
            
        }
        public Task Execute(IJobExecutionContext context)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

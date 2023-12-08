using Contracts.StatisticsMessages.Apartments;
using MassTransit;
using Microsoft.Extensions.Logging;
using Statistics.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Application.Consumers.Apartments
{
    public class ApartmentsStatisticsResultMessageConsumer : IConsumer<ApartmentsStatisticsResultMessage>
    {
        private readonly ILogger<ApartmentsStatisticsResultMessageConsumer> _logger;
        private readonly IApartmentsStatisticsRepository _apartmentsStatisticsRepository;
        public ApartmentsStatisticsResultMessageConsumer(ILogger<ApartmentsStatisticsResultMessageConsumer> logger, IApartmentsStatisticsRepository apartmentsStatisticsRepository)
        {
            _logger = logger;
            _apartmentsStatisticsRepository = apartmentsStatisticsRepository;
            
        }
        public async Task Consume(ConsumeContext<ApartmentsStatisticsResultMessage> context)
        {
            throw new NotImplementedException();
        }
    }
}

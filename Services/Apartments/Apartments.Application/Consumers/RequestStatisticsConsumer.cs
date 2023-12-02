using Amazon.Runtime.Internal.Util;
using Apartments.Domain.Interfaces;
using Contracts.StatisticsMessages;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apartments.Application.Consumers
{
    public class RequestStatisticsConsumer : IConsumer<RequestStatisticsMessage>
    {
        private readonly ILogger<RequestStatisticsConsumer> _logger;
        private readonly IApartmentsStatisticsRepository _apartmentsStatisticsRepository;
        public RequestStatisticsConsumer(ILogger<RequestStatisticsConsumer> logger, IApartmentsStatisticsRepository apartmentsStatisticsRepository)
        {
            _apartmentsStatisticsRepository = apartmentsStatisticsRepository;
            _logger = logger;
        }
        public Task Consume(ConsumeContext<RequestStatisticsMessage> context)
        {
            throw new NotImplementedException();
        }
    }
}

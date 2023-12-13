using Contracts.StatisticsMessages.Rooms;
using MassTransit;
using Microsoft.Extensions.Logging;
using Statistics.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Application.Consumers.Rooms
{
    public class RoomsStatisticsResultMessageConsumer : IConsumer<RoomsStatisticsResultMessage>
    {
        private readonly IRoomsStatisticsRepository _repository;
        private readonly ILogger<RoomsStatisticsResultMessageConsumer> _logger;

        public RoomsStatisticsResultMessageConsumer(IRoomsStatisticsRepository repository, ILogger<RoomsStatisticsResultMessageConsumer> logger)
        {
            _repository = repository;
            _logger = logger;
            
        }
        public Task Consume(ConsumeContext<RoomsStatisticsResultMessage> context)
        {
            throw new NotImplementedException();
        }
    }
}

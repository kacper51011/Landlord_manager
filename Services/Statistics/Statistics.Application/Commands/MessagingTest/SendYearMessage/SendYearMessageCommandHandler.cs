using Contracts.StatisticsMessages;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Statistics.Application.Commands.MessagingTest.SendYearMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Application.Commands.MessagingTest.SendYearMessage
{
    public class SendYearMessageCommandHandler : IRequestHandler<SendYearMessageCommand>
    {
        private readonly IPublishEndpoint _endpoint;
        private readonly ILogger<SendYearMessageCommandHandler> _logger;
        public SendYearMessageCommandHandler(IPublishEndpoint endpoint, ILogger<SendYearMessageCommandHandler> logger)
        {
            _endpoint = endpoint;
            _logger = logger;

        }
        public async Task Handle(SendYearMessageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _endpoint.Publish(new StatisticYearMessage { Year = request.dto.Year });
                _logger.LogInformation("Sent Statistic Year Message from message test controller");
            }
            catch (Exception)
            {
                _logger.LogWarning("Something went wrong while sending Statistic Year Message from message test controller");
                throw;
            }
        }
    }
}

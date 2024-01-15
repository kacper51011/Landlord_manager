using Contracts.StatisticsMessages;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Statistics.Application.Commands.MessagingTest.SendMonthMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Application.Commands.MessagingTest.SendMonthMessage
{
    public class SendMonthMessageCommandHandler : IRequestHandler<SendMonthMessageCommand>
    {
        private readonly IPublishEndpoint _endpoint;
        private readonly ILogger<SendMonthMessageCommandHandler> _logger;
        public SendMonthMessageCommandHandler(IPublishEndpoint endpoint, ILogger<SendMonthMessageCommandHandler> logger)
        {
            _endpoint = endpoint;
            _logger = logger;

        }
        public async Task Handle(SendMonthMessageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _endpoint.Publish(new StatisticMonthMessage { Month = request.dto.Month, Year = request.dto.Year });
                _logger.LogInformation("Sent Statistic Month Message from message test controller");
            }
            catch (Exception)
            {
                _logger.LogWarning("Something went wrong while sending Statistic Month Message from message test controller");
                throw;
            }
        }
    }
}

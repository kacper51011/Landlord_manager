using Contracts.StatisticsMessages;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Statistics.Application.Commands.MessagingTest.SendHourMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Application.Commands.MessagingTest.SendDayMessage
{
    public class SendDayMessageCommandHandler : IRequestHandler<SendDayMessageCommand>
    {
        private readonly IPublishEndpoint _endpoint;
        private readonly ILogger<SendDayMessageCommandHandler> _logger;
        public SendDayMessageCommandHandler(IPublishEndpoint endpoint, ILogger<SendDayMessageCommandHandler> logger)
        {
            _endpoint = endpoint;
            _logger = logger;
            
        }
        public async Task Handle(SendDayMessageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _endpoint.Publish(new StatisticDayMessage {  Day = request.dto.Day, Month = request.dto.Month, Year = request.dto.Year });
                _logger.LogInformation("Sent Statistic Day Message from message test controller");
            }
            catch (Exception)
            {
                _logger.LogWarning("Something went wrong while sending Statistic Day Message from message test controller");
                throw;
            }
        }
    }
}

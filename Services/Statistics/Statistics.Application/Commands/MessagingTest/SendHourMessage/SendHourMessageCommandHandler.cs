using Contracts.StatisticsMessages;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Statistics.Application.Commands.MessagingTest.SendHourMessage
{
    public class SendHourMessageCommandHandler : IRequestHandler<SendHourMessageCommand>
    {
        private readonly IPublishEndpoint _endpoint;
        private readonly ILogger<SendHourMessageCommandHandler> _logger;
        public SendHourMessageCommandHandler(IPublishEndpoint endpoint, ILogger<SendHourMessageCommandHandler> logger)
        {
            _endpoint = endpoint;
            _logger = logger;

        }
        public async Task Handle(SendHourMessageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _endpoint.Publish(new StatisticHourMessage { Hour = request.dto.Hour, Day = request.dto.Day, Month = request.dto.Month, Year = request.dto.Year });
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

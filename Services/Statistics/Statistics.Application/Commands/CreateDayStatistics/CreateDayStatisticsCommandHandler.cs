using MediatR;
using Statistics.Domain.Entities;
using Statistics.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Application.Commands.CreateDayStatistics
{
    public class CreateDayStatisticsCommandHandler : IRequestHandler<CreateDayStatisticsCommand>
    {
        private readonly IApartmentsStatisticsRepository _repository;
        public CreateDayStatisticsCommandHandler(IApartmentsStatisticsRepository repository)
        {
            _repository = repository;
            
        }
        public Task Handle(CreateDayStatisticsCommand request, CancellationToken cancellationToken)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
            var x = request.RequestDto;

        }
    }
}

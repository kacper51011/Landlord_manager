using Amazon.Runtime.Internal.Util;
using Apartments.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apartments.Application.Commands.DeleteApartment
{
    public class DeleteApartmentCommandHandler : IRequestHandler<DeleteApartmentCommand>
    {
        private readonly IApartmentsRepository _apartmentsRepository;
        private readonly ILogger<DeleteApartmentCommandHandler> _logger;
        public DeleteApartmentCommandHandler(IApartmentsRepository apartmentsRepository, ILogger<DeleteApartmentCommandHandler> logger)
        {
            _apartmentsRepository = apartmentsRepository;
            _logger = logger;
            
        }

        public async Task Handle(DeleteApartmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var apartment = _apartmentsRepository.GetApartmentByIdAndLandlordId(request.landlordId, request.apartmentId);
                if (apartment == null)
                {
                    throw new FileNotFoundException("Couldn`t find apartment to delete");
                }
                await _apartmentsRepository.DeleteApartment(request.apartmentId);
                return;
                
            }
            catch (FileNotFoundException ex)
            {
                _logger.LogWarning(404, ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(500, ex.Message);
                throw;
            }


        }
}
    }

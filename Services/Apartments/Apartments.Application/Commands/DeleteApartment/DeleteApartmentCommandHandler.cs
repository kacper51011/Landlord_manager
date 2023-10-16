using Apartments.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apartments.Application.Commands.DeleteApartment
{
    public class DeleteApartmentCommandHandler : IRequestHandler<DeleteApartmentCommand, bool>
    {
        private readonly IApartmentsRepository _apartmentsRepository;
        public DeleteApartmentCommandHandler(IApartmentsRepository apartmentsRepository)
        {
            _apartmentsRepository = apartmentsRepository;
            
        }

        public async Task<bool> Handle(DeleteApartmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var apartment = _apartmentsRepository.GetApartmentByIdAndLandlordId(request.landlordId, request.apartmentId);
                if (apartment == null)
                {
                    return false;
                } else
                {
                   await _apartmentsRepository.DeleteApartment(request.apartmentId);
                    return true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
}
    }

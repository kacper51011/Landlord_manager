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
        private readonly IApartmentsRepository _repository;
        public DeleteApartmentCommandHandler(IApartmentsRepository repository)
        {
            _repository = repository;
            
        }
        public bool Handle(DeleteApartmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _repository.DeleteApartment(request.userId,request.id);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}

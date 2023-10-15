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

        public async Task<bool> Handle(DeleteApartmentCommand request, CancellationToken cancellationToken)
        {
            try
            {

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
}
    }

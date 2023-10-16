using Apartments.Application.Dtos;
using Apartments.Application.Queries.GetApartments;
using Apartments.Domain.Entities;
using Apartments.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apartments.Application.Commands.CreateOrUpdateApartment
{
    public class CreateOrUpdateApartmentCommandHandler : IRequestHandler<CreateOrUpdateApartmentCommand>
    {
        private readonly IApartmentsRepository _apartmentsRepository;
        public CreateOrUpdateApartmentCommandHandler(IApartmentsRepository apartmentsRepository)
        {
            _apartmentsRepository = apartmentsRepository;
        }
        public async Task Handle(CreateOrUpdateApartmentCommand request, CancellationToken cancellationToken)
        {
            try
            {


                if (request.dto.ApartmentId == null)
                {
                    var apartment = Apartment.CreateApartment(request.dto.LandlordId, request.dto.Latitude, request.dto.Longitude, request.dto.RoomsNumber, request.dto.Area, request.dto.Telephone);
                    await _apartmentsRepository.CreateOrUpdateApartment(apartment);
                }
                else
                {
                    var apartment = await _apartmentsRepository.GetApartmentById(request.dto.ApartmentId);
                    if (apartment == null)
                    {
                        apartment = Apartment.CreateApartment(request.dto.LandlordId, request.dto.Latitude, request.dto.Longitude, request.dto.RoomsNumber, request.dto.Area, request.dto.Telephone);
                        await _apartmentsRepository.CreateOrUpdateApartment(apartment);

                    }
                    else
                    {
                        apartment.UpdateApartment(request.dto.LandlordId, request.dto.Latitude, request.dto.Longitude, request.dto.RoomsNumber, request.dto.Area, request.dto.Telephone);
                        await _apartmentsRepository.CreateOrUpdateApartment(apartment);
                    }

                    return;
                };





            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

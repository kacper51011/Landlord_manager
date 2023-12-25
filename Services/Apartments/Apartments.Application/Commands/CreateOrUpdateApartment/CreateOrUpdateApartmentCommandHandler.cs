using Amazon.Runtime.Internal.Util;
using Apartments.Application.Dtos;
using Apartments.Application.Queries.GetApartments;
using Apartments.Domain.Entities;
using Apartments.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<CreateOrUpdateApartmentCommandHandler> _logger;
        public CreateOrUpdateApartmentCommandHandler(IApartmentsRepository apartmentsRepository, ILogger<CreateOrUpdateApartmentCommandHandler> logger)
        {
            _apartmentsRepository = apartmentsRepository;
            _logger = logger;
        }
        public async Task Handle(CreateOrUpdateApartmentCommand request, CancellationToken cancellationToken)
        {
            try
            {


                if (request.dto.ApartmentId == null)
                {
                    var apartment = Apartment.CreateApartment(request.dto.LandlordId, request.dto.Latitude, request.dto.Longitude, request.dto.Area, request.dto.Telephone);
                    await _apartmentsRepository.CreateOrUpdateApartment(apartment);
                }
                else
                {
                    var apartment = await _apartmentsRepository.GetApartmentById(request.dto.ApartmentId);
                    if (apartment == null)
                    {
                        apartment = Apartment.CreateApartment(request.dto.LandlordId, request.dto.Latitude, request.dto.Longitude, request.dto.Area, request.dto.Telephone);
                        await _apartmentsRepository.CreateOrUpdateApartment(apartment);

                    }
                    else
                    {
                        apartment.UpdateApartment(request.dto.LandlordId, request.dto.Latitude, request.dto.Longitude, request.dto.Area, request.dto.Telephone);
                        await _apartmentsRepository.CreateOrUpdateApartment(apartment);
                    }

                    return;
                };


            }
            catch (Exception ex)
            {
                _logger.LogWarning(500, ex.Message);
                throw;
            }
        }
    }
}

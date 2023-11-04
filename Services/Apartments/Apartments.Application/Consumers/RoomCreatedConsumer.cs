using Apartments.Domain.Interfaces;
using Contracts.RoomsServiceEvents;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apartments.Application.Consumers
{
    public class RoomCreatedConsumer : IConsumer<RoomCreatedEvent>
    {
        private readonly IApartmentsRepository _apartmentsRepository;

        public RoomCreatedConsumer(IApartmentsRepository apartmentsRepository)
        {
            _apartmentsRepository = apartmentsRepository;
        }


        public async Task Consume(ConsumeContext<RoomCreatedEvent> context)
        {
            var apartment = await _apartmentsRepository.GetApartmentById(context.Message.apartmentId);
            if (apartment == null)
            {
                return;
            }
            apartment.IncreaseRoomNumber();
            await _apartmentsRepository.CreateOrUpdateApartment(apartment);
        }
    }
}

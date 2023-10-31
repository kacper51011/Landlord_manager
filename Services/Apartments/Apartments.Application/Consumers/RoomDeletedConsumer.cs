using Apartments.Application.Contracts;
using Apartments.Domain.Interfaces;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apartments.Application.Consumers
{
    public class RoomDeletedConsumer : IConsumer<RoomDeletedMessage>
    {
        private IApartmentsRepository _apartmentsRepository;

        public RoomDeletedConsumer(IApartmentsRepository apartmentsRepository)
        {
            _apartmentsRepository = apartmentsRepository;
        }


        public async Task Consume(ConsumeContext<RoomDeletedMessage> context)
        {
            var apartment = await _apartmentsRepository.GetApartmentById(context.Message.apartmentId);
            if (apartment == null)
            {
                return;
            }
            apartment.DecreaseRoomNumber();
            await _apartmentsRepository.CreateOrUpdateApartment(apartment);
        }
    }
}

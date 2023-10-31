using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apartments.Application.Contracts
{
    [EntityName("Contracts-RoomDeleted")]
    public record RoomDeletedMessage
    {
        public string apartmentId { get; set; }
    }
}

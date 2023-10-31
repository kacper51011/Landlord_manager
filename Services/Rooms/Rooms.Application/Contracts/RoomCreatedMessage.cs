using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rooms.Application.Contracts
{
    [EntityName("Contracts-RoomCreated")]
    public record RoomCreatedMessage
    {
        public string apartmentId { get; set; }
    }
}

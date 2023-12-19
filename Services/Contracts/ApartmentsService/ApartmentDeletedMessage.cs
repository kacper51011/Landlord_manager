using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ApartmentsServiceEvents
{
    public record ApartmentDeletedMessage
    {
        public string RoomId { get; set; }
    }
}

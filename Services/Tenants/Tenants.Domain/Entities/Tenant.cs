using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenants.Domain.Entities
{
    public class Tenant : AggregateRoot
    {
        public string RoomId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public int Age { get; private set; }
        public bool IsStudying { get; private set; }
        public bool isWorking { get; private set; }
        public string Email { get; private set; }
        public int Rent { get; private set; }
        public DateOnly ContractStart { get; private set; }
        public DateOnly ContractEnd { get; private set;}

    }
}

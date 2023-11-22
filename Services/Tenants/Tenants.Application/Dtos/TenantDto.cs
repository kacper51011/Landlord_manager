using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenants.Application.Dtos
{
    public class TenantDto
    {
        public string? TenantId { get; set; }
        public string RoomId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public bool IsStudying { get; set; }
        public bool IsWorking { get; set; }
        public string Email { get; set; }
        public int Rent { get; set; }
        public DateTime ContractStart { get; set; }
        public DateTime ContractEnd { get; set; }
        public string Telephone { get; set; }
    }
}

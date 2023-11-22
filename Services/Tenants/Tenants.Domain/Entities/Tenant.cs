using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenants.Domain.Entities
{

    public class Tenant : AggregateRoot
    {
        public string TenantId { get; private set; }
        public string RoomId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public int Age { get; private set; }
        public bool IsStudying { get; private set; }
        public bool IsWorking { get; private set; }
        public string Email { get; private set; }
        public int Rent { get; private set; }
        public DateTime ContractStart { get; private set; }
        public DateTime ContractEnd { get; private set; }
        public string Telephone { get; private set; }

        public DateTime LastCheckedDate { get; private set; }

        private Tenant(string tenantId, string roomId, string firstName, string lastName, int age, bool isStudying, bool isWorking, string email, int rent, DateTime contractStart, DateTime contractEnd, string telephone)
        {
            TenantId = tenantId;
            RoomId = roomId;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            IsStudying = isStudying;
            IsWorking = isWorking;
            Email = email;
            Rent = rent;
            ContractStart = contractStart;
            ContractEnd = contractEnd;
            Telephone = telephone;
        }
        public static Tenant Create(string roomId, string firstName, string lastName, int age, bool isStudying, bool isWorking, string email, int rent, DateTime contractStart, DateTime contractEnd, string telephone)
        {
            Tenant tenant = new Tenant(Guid.NewGuid().ToString(), roomId, firstName, lastName, age, isStudying, isWorking, email, rent, contractStart, contractEnd, telephone);
            tenant.SetCreationDate();
            tenant.SetLastModifiedDate();
            tenant.SetLastCheckedDate();
            return tenant;
        }

        public void Update(Tenant tenant)
        {
            FirstName = tenant.FirstName;
            LastName = tenant.LastName;
            Age = tenant.Age;
            IsStudying = tenant.IsStudying;
            IsWorking = tenant.IsWorking;
            Email = tenant.Email;
            Rent = tenant.Rent;
            ContractStart = tenant.ContractStart;
            ContractEnd = tenant.ContractEnd;
            Telephone = tenant.Telephone;
            SetLastModifiedDate();
        }

        public void SetLastCheckedDate()
        {
            LastCheckedDate = DateTime.UtcNow;
        }


    }
}

using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenants.Domain.Interfaces;

namespace Tenants.Application.Workers
{
    public class RoomCheckBackgroundJob : IJob
    {
        private readonly ITenantsRepository _tenantsRepository;
        public RoomCheckBackgroundJob(ITenantsRepository tenantsRepository)
        {
            _tenantsRepository = tenantsRepository;
        }
        public Task Execute(IJobExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}

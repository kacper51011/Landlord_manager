using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenants.Domain.Interfaces;

namespace Tenants.Application.Commands.DeleteAllTenantsInRoom
{
    public class DeleteAllTenantsInRoomCommandHandler : IRequestHandler<DeleteAllTenantsInRoomCommand>
    {
		private readonly ITenantsRepository _tenantsRepository;
        private readonly ILogger<DeleteAllTenantsInRoomCommandHandler> _logger;
        public DeleteAllTenantsInRoomCommandHandler(ITenantsRepository tenantsRepository, ILogger<DeleteAllTenantsInRoomCommandHandler> logger)
        {
            _tenantsRepository = tenantsRepository;
            _logger = logger;
            
        }
        public async Task Handle(DeleteAllTenantsInRoomCommand request, CancellationToken cancellationToken)
        {
			try
			{
                var tenants = await _tenantsRepository.GetTenantsByRoomId(request.roomId);
                if (tenants == null)
                {
                    _logger.LogDebug($"Cant find tenants to delete for room {request.roomId}");
                    return;
                }
                foreach (var tenant in tenants)
                {
                   await _tenantsRepository.DeleteTenant(tenant.TenantId);
                }

			}
			catch (Exception)
			{

				throw;
			}
        }
    }
}

using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenants.Application.Dtos;
using Tenants.Domain.Interfaces;

namespace Tenants.Application.Queries.GetTenants
{
    public class GetTenantsByRoomIdQueryHandler : IRequestHandler<GetTenantsByRoomIdQuery, List<TenantDto>>
    {
        private readonly ITenantsRepository _tenantRepository;
        private readonly ILogger<GetTenantsByRoomIdQueryHandler> _logger;
        public GetTenantsByRoomIdQueryHandler(ITenantsRepository tenantRepository, ILogger<GetTenantsByRoomIdQueryHandler> logger)
        {
            
            _tenantRepository = tenantRepository;
            _logger = logger;
        }
        public async Task<List<TenantDto>> Handle(GetTenantsByRoomIdQuery request, CancellationToken cancellationToken)
        {
			try
			{
                var tenants = await _tenantRepository.GetTenantsByRoomId(request.roomId);
                List<TenantDto> response = new List<TenantDto>();
                if (tenants == null)
                {
                    _logger.LogDebug($"Can`t find tenants for room with ID {request.roomId}");
                    return response;
                }
                foreach (var tenant in tenants )
                {
                    
                    TenantDto tenantDto = tenant.Adapt<TenantDto>();
                    response.Add(tenantDto);
                }
                return response;


			}
			catch (Exception)
			{

				throw;
			}
        }
    }
}

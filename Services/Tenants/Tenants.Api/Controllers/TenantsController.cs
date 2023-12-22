using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Tenants.Application.Commands.CreateOrUpdateTenant;
using Tenants.Application.Commands.DeleteTenant;
using Tenants.Application.Dtos;
using Tenants.Application.Queries.GetTenants;

namespace Tenants.Api.Controllers
{
 
        [Route("api/[controller]")]
        [ApiController]
        public class TenantsController : ControllerBase
        {
            private readonly IMediator _mediator;
            public TenantsController(IMediator mediator)
            {
                _mediator = mediator;
            }

            [HttpGet]
            public async Task<IActionResult> GetTenants(string roomId)
            {
                try
                {
                    var query = new GetTenantsByRoomIdQuery(roomId);
                    var response = await _mediator.Send(query);
                    return Ok(response);
                }
                catch (Exception)
                {

                    throw;
                }
            }

            [HttpPost]
            public async Task<IActionResult> CreateOrUpdateTenant([FromBody] TenantDto dto)
            {
                try
                {
                    var command = new CreateOrUpdateTenantCommand(dto);
                    await _mediator.Send(command);


                    return Ok();


                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    return StatusCode(500);
                }
            }
            [HttpDelete]
            public async Task<IActionResult> DeleteTenant(string roomId, string tenantId)
            {
                try
                {
                    var command = new DeleteTenantCommand(roomId, tenantId);
                    var isSuccess = await _mediator.Send(command);

                    if (!isSuccess)
                    {
                        return BadRequest();
                    }
                    return Ok();
                }
                catch (Exception ex)
                {

                    Debug.WriteLine(ex);
                    return StatusCode(500);
                }
            }
        }
    
}

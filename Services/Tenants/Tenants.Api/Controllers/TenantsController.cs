using MediatR;
using Microsoft.AspNetCore.Mvc;
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



        /// <summary>
        /// Gets List of tenants for specified roomId
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<TenantDto>>> GetTenants(string roomId)
        {
            try
            {
                var query = new GetTenantsByRoomIdQuery(roomId);
                var response = await _mediator.Send(query);
                return response;
            }
            catch (FileNotFoundException ex)
            {

                return StatusCode(404, ex.Message);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Creates or updates tenant, depends on including tenantId in body
        /// </summary>
        /// <param name="tenantDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateOrUpdateTenant([FromBody] TenantDto tenantDto)
        {
            try
            {
                var command = new CreateOrUpdateTenantCommand(tenantDto);
                await _mediator.Send(command);


                return Ok();


            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Deletes tenant with specified tenantId in room with specified roomId
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> DeleteTenant(string tenantId)
        {
            try
            {
                var command = new DeleteTenantCommand(tenantId);
                await _mediator.Send(command);
                return Ok();
            }
            catch (FileNotFoundException ex)
            {

                return StatusCode(404, ex.Message);
            }

            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }
    }

}

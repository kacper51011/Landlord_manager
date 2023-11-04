using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Tenants.Application.Dtos;

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
        [Route("{RoomId}")]
        public async Task<IActionResult> GetTenants(string roomId)
        {
            try
            {
                var query = new GetTenantsQuery(roomId);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateOrUpdateApartment([FromBody] TenantDto dto)
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
        [Route("")]
        public async Task<IActionResult> DeleteApartment(string landlordId, string id)
        {
            try
            {
                var command = new DeleteTenantCommand(landlordId, id);
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

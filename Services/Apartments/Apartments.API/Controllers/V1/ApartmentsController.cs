using Apartments.Application.Commands.CreateOrUpdateApartment;
using Apartments.Application.Commands.DeleteApartment;
using Apartments.Application.Dtos;
using Apartments.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Apartments.API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiController]
    public class ApartmentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ApartmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("")]
        public async Task GetApartments(string userId)
        {
            try
            {
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateOrUpdateApartment(string userId, ApartmentDto dto)
        {
            try
            {
                var command = new CreateOrUpdateCommand(userId, dto);
                var isSuccess = await _mediator.Send(command);

                if(!isSuccess)
                {
                    return BadRequest();
                }
                return Ok();
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }
        [HttpDelete]
        [Route("")]
        public async Task<IActionResult> DeleteApartment(string userId, string id)
        {
            try
            {
                var command = new DeleteApartmentCommand(userId, id);
                var isSuccess = await _mediator.Send(command);

                if (!isSuccess)
                {
                    return BadRequest();
                }
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }




    }
}

using Apartments.Application.Commands.CreateOrUpdateApartment;
using Apartments.Application.Commands.DeleteApartment;
using Apartments.Application.Dtos;
using Apartments.Application.Queries.GetApartment;
using Apartments.Application.Queries.GetApartments;
using Apartments.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
        [Route("/{landlordId}")]
        public async Task<IActionResult> GetApartments(string landlordId)
        {
            try
            {
                var query = new GetApartmentsQuery(landlordId);
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
        public async Task<IActionResult> CreateOrUpdateApartment([FromBody]ApartmentDto dto)
        {
            try
            {
                var command = new CreateOrUpdateApartmentCommand(dto);
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
                var command = new DeleteApartmentCommand(landlordId, id);
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
        [HttpGet]
        [Route("{landlordId}/{apartmentId}")]
        public async Task<IActionResult> GetApartment(string landlordId, string apartmentId)
        {
            try
            {
                var query = new GetApartmentQuery(landlordId, apartmentId);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex);
                return StatusCode(500);
            }
        }




    }
}

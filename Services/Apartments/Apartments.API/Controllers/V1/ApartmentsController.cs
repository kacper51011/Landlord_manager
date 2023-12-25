using Apartments.Application.Commands.CreateOrUpdateApartment;
using Apartments.Application.Commands.DeleteApartment;
using Apartments.Application.Dtos;
using Apartments.Application.Queries.GetApartments;
using MediatR;
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

        /// <summary>
        /// Gets Apartments for specified landlord
        /// </summary>
        /// <param name="landlordId">Id of specified landlord</param>
        /// <returns>List of apartments</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<List<ApartmentDto>>> GetApartments(string landlordId)
        {
            try
            {
                var query = new GetApartmentsQuery(landlordId);
                var response = await _mediator.Send(query);
                return Ok(response);
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
        /// Creates or updates apartment, it depends on providing Id of apartment
        /// </summary>
        /// <param name="apartmentDto">object needed for creating or updating apartment in db</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateOrUpdateApartment([FromBody] ApartmentDto apartmentDto)
        {
            try
            {
                var command = new CreateOrUpdateApartmentCommand(apartmentDto);
                await _mediator.Send(command);


                return Ok();


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Deletes apartment if landlordId and apartmentId matches
        /// </summary>
        /// <param name="landlordId"></param>
        /// <param name="apartmentId"></param>
        /// <returns>Returns 200 status code if deleted</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteApartment(string landlordId, string apartmentId)
        {
            try
            {
                var command = new DeleteApartmentCommand(landlordId, apartmentId);
                await _mediator.Send(command);

                return Ok();
            }
            catch (FileNotFoundException ex)
            {

                return StatusCode(404, ex.Message);
            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex);
                return StatusCode(500);
            }
        }
        //[HttpGet]
        //public async Task<IActionResult> GetApartment(string landlordId, string apartmentId)
        //{
        //    try
        //    {
        //        var query = new GetApartmentQuery(landlordId, apartmentId);
        //        var response = await _mediator.Send(query);
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {

        //        Debug.WriteLine(ex);
        //        return StatusCode(500);
        //    }
        //}




    }
}

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rooms.Application.Commands.CreateOrUpdateRoom;
using Rooms.Application.Commands.DeleteRoom;
using Rooms.Application.Dto;
using Rooms.Application.Queries.GetRooms;

namespace Rooms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RoomsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets rooms located in specified apartment
        /// </summary>
        /// <param name="apartmentId">Id of apartment</param>
        /// <returns>List of rooms</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<List<RoomDto>>> GetApartmentRooms(string apartmentId)
        {
            try
            {
                var query = new GetRoomsQuery(apartmentId);
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
        /// Responsible for creating room or updating already existing one
        /// </summary>
        /// <param name="roomDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateOrUpdateRoom(RoomDto roomDto)
        {
            try
            {
                var command = new CreateOrUpdateRoomCommand(roomDto);
                await _mediator.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// Responsible for deleting room in apartment
        /// </summary>
        /// <param name="landlordId"></param>
        /// <param name="apartmentId"></param>
        /// <param name="roomId"></param>
        /// <returns>Returns status code 200 if operation succeeded</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("{roomId}")]
        public async Task<IActionResult> DeleteRoom(string landlordId, string apartmentId, string roomId)
        {
            try
            {
                var query = new DeleteRoomCommand(landlordId, apartmentId, roomId);
                await _mediator.Send(query);
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

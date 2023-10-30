﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rooms.Application.Commands.CreateOrUpdateRoom;
using Rooms.Application.Commands.DeleteAllRooms;
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

        [HttpGet]
        [Route("{apartmentId}")]
        public async Task<IActionResult> GetApartmentRooms(string apartmentId)
        {
            try
            {
                var query = new GetRoomsQuery(apartmentId);
                var response = await _mediator.Send(query);
                return Ok(response);

            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        [Route("Create")]
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
        [HttpDelete]
        [Route("{roomId}")]
        public async Task<IActionResult> DeleteRoom(string landlordId, string roomId)
        {
            try
            {
                var query = new DeleteRoomCommand(landlordId, roomId);
                await _mediator.Send(query);
                return Ok();

            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete]
        [Route("{apartmentId}")]
        public async Task<IActionResult> DeleteAllRoomsInApartment(string landlordId, string apartmentId)
        {
            try
            {
                var query = new DeleteAllRoomsCommand(landlordId, apartmentId);
                await _mediator.Send(query);
                return Ok();

            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }
    }
}

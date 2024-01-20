using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Statistics.Application.Commands.Apartments.CreateHourStatistics;
using Statistics.Application.Commands.Apartments.CreateYearStatistics;
using Statistics.Application.Commands.Rooms.CreateDayStatistics;
using Statistics.Application.Commands.Rooms.CreateHourStatistics;
using Statistics.Application.Commands.Rooms.CreateMonthStatistics;
using Statistics.Application.Commands.Rooms.CreateYearStatistics;
using Statistics.Application.Dto.In;
using Statistics.Application.Dto.Out;
using Statistics.Application.Queries.Rooms;
using Statistics.Domain.Entities;
using System.Data;

namespace Statistics.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsStatisticsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoomsStatisticsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Route("RoomStatistic")]

        public async Task<ActionResult<GetRoomsStatisticResponse>> GetAnyRoomStatistic([FromQuery] GetStatisticDto dto)
        {
            try
            {
                var query = new GetRoomStatisticQuery(dto);
                var response = await _mediator.Send(query);

                return Ok(response);
            }
            catch (FileNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (FileLoadException ex)
            {
                return StatusCode(423, ex.Message);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("HourStatistics")]
        public async Task<ActionResult> CreateHourStatistics(CreateHourStatisticsRequestDto statisticsRequestDto)
        {
            try
            {
                var command = new CreateRoomHourStatisticsCommand(statisticsRequestDto);
                await _mediator.Send(command);

                return Ok();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (DuplicateNameException ex)
            {
                return StatusCode(403, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        [Route("DayStatistics")]
        public async Task<ActionResult> CreateDayStatistics(CreateDayStatisticsRequestDto statisticsRequestDto)
        {
            try
            {
                var command = new CreateRoomDayStatisticsCommand(statisticsRequestDto);
                await _mediator.Send(command);

                return Ok();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (DuplicateNameException ex)
            {
                return StatusCode(403, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [HttpPost]
        [Route("MonthStatistics")]
        public async Task<ActionResult> CreateMonthStatistics(CreateMonthStatisticsRequestDto statisticsRequestDto)
        {
            try
            {
                var command = new CreateRoomMonthStatisticsCommand(statisticsRequestDto);
                await _mediator.Send(command);

                return Ok();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (DuplicateNameException ex)
            {
                return StatusCode(403, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        [Route("YearStatistics")]
        public async Task<ActionResult> CreateYearStatistics(CreateYearStatisticsRequestDto statisticsRequestDto)
        {
            try
            {
                var command = new CreateRoomYearStatisticsCommand(statisticsRequestDto);
                await _mediator.Send(command);

                return Ok();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (DuplicateNameException ex)
            {
                return StatusCode(403, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}

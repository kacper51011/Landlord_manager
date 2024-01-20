using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Statistics.Application.Commands.Apartments.CreateDayStatistics;
using Statistics.Application.Commands.Apartments.CreateHourStatistics;
using Statistics.Application.Commands.Apartments.CreateMonthStatistics;
using Statistics.Application.Commands.Apartments.CreateYearStatistics;
using Statistics.Application.Dto.In;
using System.Data;

namespace Statistics.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentsStatisticsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ApartmentsStatisticsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [Route("HourStatistics")]
        public async Task<ActionResult> CreateHourStatistics(CreateHourStatisticsRequestDto statisticsRequestDto)
        {
            try
            {
                var command = new CreateApartmentHourStatisticsCommand(statisticsRequestDto);
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
                var command = new CreateApartmentDayStatisticsCommand(statisticsRequestDto);
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
                var command = new CreateApartmentMonthStatisticsCommand(statisticsRequestDto);
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
                var command = new CreateApartmentYearStatisticsCommand(statisticsRequestDto);
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

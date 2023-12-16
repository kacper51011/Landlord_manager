using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Statistics.Application.Commands.Apartments.CreateYearStatistics;
using Statistics.Application.Commands.Rooms.CreateYearStatistics;
using Statistics.Application.Commands.Tenants.CreateDayStatistics;
using Statistics.Application.Commands.Tenants.CreateHourStatistics;
using Statistics.Application.Commands.Tenants.CreateMonthStatistics;
using Statistics.Application.Commands.Tenants.CreateYearStatistics;
using Statistics.Application.Dto;
using System.Data;

namespace Statistics.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantsStatisticsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TenantsStatisticsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [Route("CreateHourStatisticsManually")]
        public async Task<ActionResult> CreateHourStatistics(CreateHourStatisticsRequestDto statisticsRequestDto)
        {
            try
            {
                var command = new CreateTenantHourStatisticsCommand(statisticsRequestDto);
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
        [Route("CreateDayStatisticsManually")]
        public async Task<ActionResult> CreateDayStatistics(CreateDayStatisticsRequestDto statisticsRequestDto)
        {
            try
            {
                var command = new CreateTenantDayStatisticsCommand(statisticsRequestDto);
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
        [Route("CreateMonthStatisticsManually")]
        public async Task<ActionResult> CreateMonthStatistics(CreateMonthStatisticsRequestDto statisticsRequestDto)
        {
            try
            {
                var command = new CreateTenantMonthStatisticsCommand(statisticsRequestDto);
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
        [Route("CreateYearStatisticsManually")]
        public async Task<ActionResult> CreateYearStatistics(CreateYearStatisticsRequestDto statisticsRequestDto)
        {
            try
            {
                var command = new CreateTenantYearStatisticsCommand(statisticsRequestDto);
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

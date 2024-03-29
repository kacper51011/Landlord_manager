﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Statistics.Application.Commands.Apartments.CreateYearStatistics;
using Statistics.Application.Commands.Rooms.CreateYearStatistics;
using Statistics.Application.Commands.Tenants.CreateDayStatistics;
using Statistics.Application.Commands.Tenants.CreateHourStatistics;
using Statistics.Application.Commands.Tenants.CreateMonthStatistics;
using Statistics.Application.Commands.Tenants.CreateYearStatistics;
using Statistics.Application.Dto.In;
using Statistics.Application.Dto.Out;
using Statistics.Application.Queries.Apartments.GetApartmentsStatisticQuery;
using Statistics.Application.Queries.Tenants;
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
        [HttpGet]
        [Route("TenantStatistic")]
        public async Task<ActionResult<GetTenantsStatisticResponse>> GetAnyTenantStatistic([FromQuery] GetStatisticDto dto)
        {
            try
            {
                var query = new GetTenantStatisticQuery(dto);
                var response = await _mediator.Send(query);

                return Ok(response);
            }
            catch (FileNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(FileLoadException ex)
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
        [HttpPost]
        [Route("DayStatistics")]
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
        [HttpPost]
        [Route("MonthStatistics")]
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
        [HttpPost]
        [Route("YearStatistics")]
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

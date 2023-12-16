using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Statistics.Application.Commands.Apartments.CreateYearStatistics;
using Statistics.Application.Dto;
using System.Data;

namespace Statistics.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsStatisticsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StatisticsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [Route("CreateHourStatisticsManually")]
        public async Task<ActionResult> CreateHourStatistics(CreateHourStatisticsRequestDto statisticsRequestDto)
        {
            try
            {

            }
            catch (ArgumentOutOfRangeException)
            {

            }
            catch (DuplicateNameException)
            {

            }
            catch (Exception ex)
            {


            }
        }
        [Route("CreateDayStatisticsManually")]
        public async Task<ActionResult> CreateDayStatistics(CreateDayStatisticsRequestDto statisticsRequestDto)
        {
            try
            {

            }
            catch (ArgumentOutOfRangeException)
            {

            }
            catch (DuplicateNameException)
            {

            }
            catch (Exception ex)
            {


            }

        }
        [Route("CreateMonthStatisticsManually")]
        public async Task<ActionResult> CreateMonthStatistics(CreateMonthStatisticsRequestDto statisticsRequestDto)
        {
            try
            {

            }
            catch (ArgumentOutOfRangeException)
            {

            }
            catch (DuplicateNameException)
            {

            }
            catch (Exception ex)
            {


            }
        }
        [Route("CreateYearStatisticsManually")]
        public async Task<ActionResult> CreateYearStatistics(CreateYearStatisticsRequestDto statisticsRequestDto)
        {
            try
            {
                var command = new CreateApartmentYearStatisticsCommand(statisticsRequestDto);
                await _mediator.Send(command);

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

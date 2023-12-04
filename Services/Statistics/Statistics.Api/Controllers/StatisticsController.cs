using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Statistics.Application.Dto;

namespace Statistics.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<StatisticsResponseDto>> GetStatistics(CreateHourStatisticsRequestDto statisticsRequestDto)
        {

        }
        [HttpPost]
        [Route("CreateHourStatisticsManually")]
        public async Task<ActionResult> CreateHourStatistics(CreateHourStatisticsRequestDto statisticsRequestDto)
        {

        }
        [Route("CreateDayStatisticsManually")]
        public async Task<ActionResult> CreateDayStatistics(CreateHourStatisticsRequestDto statisticsRequestDto)
        {

        }
        [Route("CreateMonthStatisticsManually")]
        public async Task<ActionResult> CreateMonthStatistics(CreateHourStatisticsRequestDto statisticsRequestDto)
        {
        }
        [Route("CreateYearStatisticsManually")]
        public async Task<ActionResult> CreateYearStatistics(CreateHourStatisticsRequestDto statisticsRequestDto)
        {

        }
    }
}

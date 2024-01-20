using MediatR;
using Microsoft.AspNetCore.Mvc;
using Statistics.Application.Commands.MessagingTest.SendDayMessage;
using Statistics.Application.Commands.MessagingTest.SendHourMessage;
using Statistics.Application.Commands.MessagingTest.SendMonthMessage;
using Statistics.Application.Commands.MessagingTest.SendYearMessage;
using Statistics.Application.Dto.In;

namespace Statistics.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagingTestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MessagingTestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("HourMessage")]
        public async Task<IActionResult> CreateHourAsyncMessage(CreateHourStatisticsRequestDto dto)
        {
            try
            {
                var command = new SendHourMessageCommand(dto);
                await _mediator.Send(command);

                return Ok();

            }
            catch (Exception)
            {

                throw;
            }

        }
        [HttpPost]
        [Route("DayMessage")]
        public async Task<IActionResult> CreateDayAsyncMessage(CreateDayStatisticsRequestDto dto)
        {
            try
            {
                var command = new SendDayMessageCommand(dto);
                await _mediator.Send(command);

                return Ok();
            }
            catch (Exception)
            {

                throw;
            }

        }
        [HttpPost]
        [Route("MonthMessage")]
        public async Task<IActionResult> CreateMonthAsyncMessage(CreateMonthStatisticsRequestDto dto)
        {
            try
            {
                var command = new SendMonthMessageCommand(dto);
                await _mediator.Send(command);

                return Ok();
            }
            catch (Exception)
            {

                throw;
            }

        }
        [HttpPost]
        [Route("YearMessage")]
        public async Task<ActionResult> CreateYearAsyncMessage(CreateYearStatisticsRequestDto dto)
        {
            try
            {
                var command = new SendYearMessageCommand(dto);
                await _mediator.Send(command);

                return Ok();
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}

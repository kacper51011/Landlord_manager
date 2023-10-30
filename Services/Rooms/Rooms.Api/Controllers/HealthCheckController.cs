using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rooms.Domain.Interfaces;

namespace Rooms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        private readonly IRoomsRepository _roomsRepository;
        public HealthCheckController(IRoomsRepository roomsRepository)
        {
            _roomsRepository = roomsRepository;
        }
        [HttpGet]
        [Route("Check")]
        public async Task<IActionResult> HealthCheck()
        {
            try
            {
                await _roomsRepository.GetRoomById("asd");
                return Ok();
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [Route("/{apartmentId}")]
        public async Task<IActionResult> GetApartmentRooms(string apartmentId)
        {

        }
    }
}

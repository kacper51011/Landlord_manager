using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Apartments.API.Controllers.V2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiController]
    public class ApartmentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ApartmentsController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetApartments(string userId)
        {

            return Ok("asd");
        }

        [HttpPost]
        [Route("")]
        public async Task CreateApartment(string userId)
        {
            return;
        }
        [HttpPut]
        [Route("")]
        public async Task EditApartment(string userId)
        {
            return;
        }
        [HttpDelete]
        [Route("")]
        public async Task DeleteApartment(string userId)
        {
            return;
        }




    }
}

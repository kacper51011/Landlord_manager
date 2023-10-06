using Apartments.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Apartments.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ApartmentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ApartmentsController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        [Route("/")]
        public async Task GetApartments(string userId)
        {

            return;
        }

        [HttpPost]
        [Route("/")]
        public async Task CreateApartment(string userId)
        {
            return;
        }
        [HttpPut]
        [Route("/")]
        public async Task EditApartment(string userId)
        {
            return;
        }
        [HttpDelete]
        [Route("/")]
        public async Task DeleteApartment(string userId)
        {
            return;
        }




    }
}

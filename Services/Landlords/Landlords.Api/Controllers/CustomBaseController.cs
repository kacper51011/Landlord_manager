using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Landlords.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class CustomBaseController : ControllerBase
    {

    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LSB.API.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class PingController : Controller
    {
        // GET: api/Ping
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("SCEToolkits Service is online!!");
        }

        // GET: api/Ping
        [HttpGet]
        [Authorize]
        [Route("getstatus")]
        public IActionResult GetStatus()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Ok("SCEToolkits Service is online!! And User is Authenticated!!!");
            }
            else
            {
                return Ok("SCEToolkits Service is online!! But the User is not Authenticated!!!");
            }
        }

    }
}

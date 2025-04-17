using Microsoft.AspNetCore.Mvc;

namespace romrepo.win.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppController : ControllerBase
    {

        [HttpGet("status")]
        public ActionResult<string> GetStatus()
        {
            return Ok("ok");
        }

        [HttpGet("version")]
        public ActionResult<string> GetVersion()
        {
            return Ok("0.0.1");
        }
    }
}

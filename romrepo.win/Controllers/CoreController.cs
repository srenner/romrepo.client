using Microsoft.AspNetCore.Mvc;
using romrepo.lib.Models;
using romrepo.lib.Services.Interfaces;

namespace romrepo.win.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoreController : ControllerBase
    {
        private readonly ILogger<CoreController> _logger;
        private readonly ICoreService _coreService;

        public CoreController(ILogger<CoreController> logger, ICoreService coreService)
        {
            _logger = logger;
            _coreService = coreService;
        }

        [HttpGet("discover")]
        public async Task<ActionResult<IEnumerable<Core>>> GetDiscoveredCores()
        {
            var cores = await _coreService.GetDiscoveredCores();
            return Ok(cores);
        }

        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<Core>>> GetActiveCores()
        {
            var cores = await _coreService.GetActiveCores();
            return Ok(cores);
        }

        [HttpGet("inactive")]
        public async Task<ActionResult<IEnumerable<Core>>> GetInactiveCores()
        {
            var cores = await _coreService.GetInactiveCores();
            return Ok(cores);
        }
    }
}

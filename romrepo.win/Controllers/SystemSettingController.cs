using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using romrepo.lib.Models;
using romrepo.lib.Models.NotMapped;
using romrepo.lib.Services.Interfaces;

namespace romrepo.win.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemSettingController : ControllerBase
    {
        private ILogger<SystemSettingController> _logger;
        private readonly IAppService _appService;

        public SystemSettingController(ILogger<SystemSettingController> logger, IAppService appService)
        {
            _logger = logger;
            _appService = appService;
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<SystemSetting?>> GetSystemSetting(string name)
        {
            var settings = await _appService.GetSystemSettings(updateCache: false);
            var setting = settings.Where(w => w.Name == name).FirstOrDefault();
            return setting == null ? StatusCode(404) : setting;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SystemSetting>>> GetSystemSettings()
        {
            var settings = await _appService.GetSystemSettings(updateCache: false);
            return Ok(settings);
        }

        [HttpPost]
        public async Task<ActionResult> SaveSystemSetting([FromBody] SystemSettingPostModel setting)
        {
            var updatedSetting = await _appService.SaveSystemSetting(setting.Name, setting.Value, updateCache: true);
            if (updatedSetting != null)
            {
                return Ok(updatedSetting);
            }
            else
            {
                return StatusCode(500);
            }
        }
    }
}

using romrepo.lib.Models;
using romrepo.lib.Services;
using romrepo.lib.Services.Interfaces;
using System.Runtime;

namespace romrepo.win
{
    public class Worker : IScopedProcessingService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ICoreService _coreService;
        private readonly IAppService _appService;
        private List<SystemSetting> _settings;


        public Worker(ILogger<Worker> logger, ICoreService coreService, IAppService appService)
        {
            _logger = logger;
            _coreService = coreService;
            _appService = appService;
        }

        private async Task<bool> InitAsync()
        {
            _settings = await _appService.InitSystemSettings();

            var uniqueIDSetting = _settings.Where(f => f.Name == SystemSettingEnum.UniqueIdentifier.Value).FirstOrDefault();
            if (string.IsNullOrWhiteSpace(uniqueIDSetting.Value))
            {
                string uniqueID = Guid.NewGuid().ToString();
                _settings = await _appService.SaveSystemSetting(SystemSettingEnum.UniqueIdentifier.Value, uniqueID, updateCache: true);
            }





            _logger.LogInformation("Worker initialized at: {time}", DateTimeOffset.Now);
            return true;
        }

        public async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if(await InitAsync())
            {

                //var newCores = await _coreService.DiscoverCores();
                //if (newCores?.Count() > 0)
                //{
                //    foreach (var coreFolder in newCores)
                //    {
                        
                //        var core = coreFolder.FromDirectoryInfo();
                //        if (core != null)
                //        {
                //            await _coreService.AddCore(core);
                //        }
                //    }
                //}
                var cores = await _coreService.GetActiveCores();



                while (!stoppingToken.IsCancellationRequested)
                {
                    //if (_logger.IsEnabled(LogLevel.Information))
                    //{
                    //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    //}
                    await Task.Delay(1000, stoppingToken);
                }
            }
        }
    }
}

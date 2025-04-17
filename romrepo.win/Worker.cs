using romrepo.lib.Models;
using romrepo.lib.Services;
using romrepo.lib.Services.Interfaces;

namespace romrepo.win
{
    public class Worker : IScopedProcessingService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ICoreService _coreService;

        public Worker(ILogger<Worker> logger, ICoreService coreService)
        {
            _logger = logger;
            _coreService = coreService;
        }

        private async Task<bool> InitAsync()
        {
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

using romrepo.lib.Models;
using romrepo.lib.Services.Interfaces;

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
                _logger.LogInformation("Unique ID set to: {uniqueID}", uniqueID);
            }

            _logger.LogInformation("Worker initialized at: {time}", DateTimeOffset.Now);
            return true;
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            if(await InitAsync())
            {
                var rootFolder = _settings.Find(f => f.Name == SystemSettingEnum.RomRootFolder.Value).Value;
                if(!string.IsNullOrWhiteSpace(rootFolder))
                {
                    int newCoreCount = await _coreService.AutoAddDiscoveredCores(rootFolder, cancellationToken);
                    if (newCoreCount > 0)
                    {
                        _logger.LogInformation("Added {newCoreCount} new cores.", newCoreCount);
                    }
                    else
                    {
                        _logger.LogInformation("No new cores found.");
                    }

                    using var watcher = BuildFileSystemWatcher(rootFolder, cancellationToken);

                    while (!cancellationToken.IsCancellationRequested)
                    {
                        await Task.Delay(1, cancellationToken);
                    }
                }
                else
                {
                    _logger.LogError("Root folder not set in settings.");
                    return;
                }
            }
        }

        private FileSystemWatcher BuildFileSystemWatcher(string rootFolder, CancellationToken cancellationToken)
        {
            if(Directory.Exists(rootFolder) == false)
            {
                _logger.LogError("Root folder does not exist: {rootFolder}", rootFolder);
                return null;
            }
            FileSystemWatcher watcher = new FileSystemWatcher(rootFolder);
            watcher.IncludeSubdirectories = true;
            watcher.NotifyFilter =
                  NotifyFilters.Attributes
                | NotifyFilters.CreationTime
                | NotifyFilters.DirectoryName
                | NotifyFilters.FileName
                | NotifyFilters.LastAccess
                | NotifyFilters.LastWrite
                | NotifyFilters.Security
                | NotifyFilters.Size;

            watcher.Changed += OnChanged;
            watcher.Created += OnCreated;
            watcher.Deleted += OnDeleted;
            watcher.Renamed += OnRenamed;
            watcher.Error += OnError;

            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;

            return watcher;
        }


        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            _logger.LogInformation($"File: {e.FullPath} {e.ChangeType}");
        }
        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            _logger.LogInformation($"File: {e.FullPath} {e.ChangeType}");
        }
        private void OnDeleted(object sender, FileSystemEventArgs e)
        {
            _logger.LogInformation($"File: {e.FullPath} {e.ChangeType}");
        }
        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            _logger.LogInformation($"File: {e.OldFullPath} renamed to {e.FullPath}");
        }
        private void OnError(object sender, ErrorEventArgs e)
        {
            _logger.LogError($"File: {e.GetException().Message}");
        }
    }
}

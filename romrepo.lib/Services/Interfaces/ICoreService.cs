using romrepo.lib.Models;

namespace romrepo.lib.Services.Interfaces
{
    public interface ICoreService
    {
        Task<IEnumerable<Core>> GetAllCores();
        Task<IEnumerable<Core>> GetActiveCores();
        Task<IEnumerable<Core>> GetInactiveCores();
        Task<IEnumerable<Core>> GetDiscoveredCores();
        Task<int> AutoAddDiscoveredCores(string rootFolder, CancellationToken cancellationToken);
        Task<IEnumerable<DirectoryInfo>> DiscoverCores(string rootFolder, CancellationToken cancellationToken);
        List<Core> GetFileSystemCores();
        Task<Core?> GetCore(int coreID);
        Task<Core> AddCore(Core core);
        Task<int> AddCores(List<Core> cores);

        Task<bool> UpdateCore(Core core);
    }
}

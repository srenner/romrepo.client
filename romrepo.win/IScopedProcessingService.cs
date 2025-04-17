namespace romrepo.win
{
    public interface IScopedProcessingService
    {
        Task ExecuteAsync(CancellationToken stoppingToken);

    }
}

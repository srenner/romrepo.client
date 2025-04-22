namespace romrepo.win
{
    /// <summary>
    /// Launches the IScopedProcessingService that was defined in Program.cs
    /// </summary>
    /// <param name="serviceScopeFactory"></param>
    /// <param name="logger"></param>
    public sealed class ScopedBackgroundService(
        IServiceScopeFactory serviceScopeFactory,
        ILogger<ScopedBackgroundService> logger) : BackgroundService
    {
        private const string ClassName = nameof(ScopedBackgroundService);

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation(
                "{Name} is running.", ClassName);

            await DoWorkAsync(cancellationToken);
        }

        private async Task DoWorkAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation(
                "{Name} is working.", ClassName);

            using (IServiceScope scope = serviceScopeFactory.CreateScope())
            {
                IScopedProcessingService scopedProcessingService =
                    scope.ServiceProvider.GetRequiredService<IScopedProcessingService>();

                await scopedProcessingService.ExecuteAsync(cancellationToken);
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation(
                "{Name} is stopping.", ClassName);

            await base.StopAsync(cancellationToken);
        }
    }
}

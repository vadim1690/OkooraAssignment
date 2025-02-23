using Microsoft.Extensions.Hosting;
using RateFetcher.Services.RateFetcherService;


namespace RateFetcher.Workers
{
    public class RateFetcherWorker : BackgroundService
    {
        private readonly IRateFetcherService _rateFetcherService;
        private readonly TimeSpan _delayTime  = TimeSpan.FromSeconds(1);

        public RateFetcherWorker(IRateFetcherService rateFetcherService)
        {
            _rateFetcherService = rateFetcherService;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await _rateFetcherService.ProcessRateFetching();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                await Task.Delay(_delayTime, stoppingToken);
            }
        }
    }
}

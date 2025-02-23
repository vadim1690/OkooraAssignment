using Domain.Data;
using Domain.Data.Configuration;
using Domain.Data.Repositories.RatePairRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RateFetcher.Services.RateFetcherService;
using RateFetcher.Workers;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.InitializeDb();

        services.AddScoped<IRateFetcherService,RateFetcherService>();
        services.AddHttpClient();
        services.AddHostedService<RateFetcherWorker>();
    })
    .Build();

host.EnsureDatabase();

await host.RunAsync();
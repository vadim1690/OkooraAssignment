using Domain.Data.Repositories.RatePairRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Domain.Data.Configuration
{
     
    public static class DbConfiguration
    {
        private static readonly string connectionString = "Server=localhost;Database=RateDb;User Id=sa;Password=Vadim12345678;TrustServerCertificate=True";
        public static IServiceCollection InitializeDb(
            this IServiceCollection services)
        {
            services.AddDbContextFactory<RateDbContext>(options =>
            {
                options.UseSqlServer(
                    connectionString,
                    sqlOptions => sqlOptions.MigrationsAssembly("Domain"));
            });

            services.AddScoped<IRatePairRepository,RatePairRepository>();
            return services;
        }
        public static IServiceCollection EnsureDatabase(this IServiceCollection services)
        {
            using var scope = services.BuildServiceProvider().CreateScope();
            var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<RateDbContext>>();
            using var db = contextFactory.CreateDbContext();
            db.Database.EnsureCreated();
            return services;
        }

        public static IHost EnsureDatabase(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<RateDbContext>>();
            using var db = contextFactory.CreateDbContext();
            db.Database.EnsureCreated();

            return host;
        }
    }
}

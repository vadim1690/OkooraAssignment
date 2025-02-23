using Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Domain.Data.Repositories.RatePairRepository
{
    // Repository for managing rate pairs in the database
    // Handling concurrency with creating instances of DbContext for each operation
    public class RatePairRepository : IRatePairRepository
    {
        private readonly IDbContextFactory<RateDbContext> _dbContextFactory;

        public RatePairRepository(IDbContextFactory<RateDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<RatePair?> GetRatePairByNameAsync(string pairName)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            return await context.Rates
                .FirstOrDefaultAsync(ratePair => ratePair.PairName == pairName);
        }

        public async Task UpsertRatePairAsync(RatePair ratePair)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            var existingRatePair = await context.Rates
                .FirstOrDefaultAsync(r => r.PairName == ratePair.PairName);

            if (existingRatePair == null)
            {
                await context.Rates.AddAsync(ratePair);
            }
            else
            {
                existingRatePair.Rate = ratePair.Rate;
                existingRatePair.LastUpdate = ratePair.LastUpdate;
            }

            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RatePair>> GetAllRatePairsAsync()
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            return await context.Rates.ToListAsync();
        }
    }
}
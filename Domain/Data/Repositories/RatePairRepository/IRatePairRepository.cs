using Domain.Data.Entities;

namespace Domain.Data.Repositories.RatePairRepository
{
    public interface IRatePairRepository
    {
        // Gets a RatePair by its PairName.
        Task<RatePair?> GetRatePairByNameAsync(string pairName);
        // Inserts a new RatePair or updates it if it already exists (Upsert).
        Task UpsertRatePairAsync(RatePair ratePair);
        // Get all RatePairs.
        Task<IEnumerable<RatePair>> GetAllRatePairsAsync();
    }
}

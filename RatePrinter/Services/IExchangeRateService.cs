using Domain.Data.Entities;
using RatePrinter.Models;

namespace RatePrinter.Services
{
    public interface IExchangeRateService
    {
        Task<IEnumerable<RatePair>> GetAllExchangeRatesAsync();
        Task<RatePair?> GetExchangeRateAsync(GetRateRequest request);
    }
}
using Domain.Data.Entities;
using Domain.Data.Repositories.RatePairRepository;
using RatePrinter.Exceptions;
using RatePrinter.Models;

namespace RatePrinter.Services
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly IRatePairRepository _repository;

        public ExchangeRateService(IRatePairRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<RatePair>> GetAllExchangeRatesAsync()
        {
            return await _repository.GetAllRatePairsAsync();
        }

        public async Task<RatePair?> GetExchangeRateAsync(GetRateRequest request)
        {
            var pairName = $"{request.BaseCurrencyCode}/{request.TargetCurrencyCode}";
            var rate = await _repository.GetRatePairByNameAsync(pairName);
            if(rate == null)
            {
                throw new NotFoundException($"Exchange rate for pair '{pairName}' not found");
            }
            return rate;
        }
    }
}

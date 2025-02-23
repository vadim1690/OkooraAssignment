using Domain.Data.Entities;
using Domain.Data.Repositories.RatePairRepository;
using Microsoft.Extensions.Configuration;
using RateFetcher.DataMappers;
using RateFetcher.Models;
using RateFetcher.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RateFetcher.Services.RateFetcherService
{

    public class RateFetcherService : IRateFetcherService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IRatePairRepository _ratePairRepository;
        private readonly string[] PairNames = ["USD/ILS","EUR/ILS","GBP/ILS","EUR/USD","EUR/GBP"];
        private readonly string _apiKey = "d0ff98fa00ec8b89df2acae3";

        public RateFetcherService(IHttpClientFactory httpClientFactory, IConfiguration configuration, IRatePairRepository ratePairRepository)
        {
            _httpClientFactory = httpClientFactory;
            _ratePairRepository = ratePairRepository;
        }

        public async Task ProcessRateFetching()
        {
            var fetchTasks = PairNames.Select(FetchRate);
            await Task.WhenAll(fetchTasks); 
        }

        public async Task FetchRate(string pairName)
        {
            try
            {
                using var client = _httpClientFactory.CreateClient();
                var url = $"https://v6.exchangerate-api.com/v6/{_apiKey}/pair/{pairName}";

                var response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Status code - {response.StatusCode}, {response.ReasonPhrase}");
                }
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var exchangeRateResponse = JsonUtils.Deserialize<ExchangeRateResponse>(json);
                    if (exchangeRateResponse == null)
                    {
                        throw new Exception("Error Deserializing Json response");
                    }
                    var ratePair = exchangeRateResponse.ToRatePair();
                    await _ratePairRepository.UpsertRatePairAsync(ratePair);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching rate for {pairName}: {ex.Message}");
            }
        }
    }
}

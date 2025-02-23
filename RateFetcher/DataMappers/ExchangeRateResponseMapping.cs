using Domain.Data.Entities;
using RateFetcher.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateFetcher.DataMappers
{
    public static class ExchangeRateResponseMapping
    {
        public static RatePair ToRatePair(this ExchangeRateResponse exchangeRateResponse)
        {
            return new RatePair
            {
                PairName = $"{exchangeRateResponse.BaseCode}/{exchangeRateResponse.TargetCode}",
                Rate = exchangeRateResponse.ConversionRate,
                LastUpdate = DateTimeOffset.FromUnixTimeSeconds(exchangeRateResponse.TimeLastUpdateUnix).DateTime
            };
        }
    }
}

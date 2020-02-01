﻿using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.DDDAPISample.Domain.ForeignExchange;
using TotalNetCore.DDDAPISample.Infrastructure.Caching;

namespace TotalNetCore.DDDAPISample.Infrastructure.Domain.ForeignExchange
{
    public class ForeignExchange : IForeignExchange
    {
        private readonly ICacheStore _cacheStore;

        public ForeignExchange(ICacheStore cacheStore)
        {
            _cacheStore = cacheStore;
        }

        public List<ConversionRate> GetConversionRates()
        {
            var ratesCache = this._cacheStore.Get(new ConversionRatesCacheKey());

            if (ratesCache != null)
            {
                return ratesCache.Rates;
            }

            List<ConversionRate> rates = GetConversionRatesFromExternalApi();

            this._cacheStore.Add(new ConversionRatesCache(rates), new ConversionRatesCacheKey(), DateTime.Now.Date.AddDays(1));

            return rates;
        }

        private static List<ConversionRate> GetConversionRatesFromExternalApi()
        {
            // Communication with external API. Here is only mock.

            var conversionRates = new List<ConversionRate>();

            conversionRates.Add(new ConversionRate("USD", "EUR", (decimal)0.88));
            conversionRates.Add(new ConversionRate("EUR", "USD", (decimal)1.13));

            return conversionRates;
        }
    }
}

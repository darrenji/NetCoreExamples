using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.DDDAPISample.Infrastructure.Caching;

namespace TotalNetCore.DDDAPISample.Infrastructure.Domain.ForeignExchange
{
    public class ConversionRatesCacheKey : ICacheKey<ConversionRatesCache>
    {
        public string CacheKey => "ConversionRatesCache";
    }
}

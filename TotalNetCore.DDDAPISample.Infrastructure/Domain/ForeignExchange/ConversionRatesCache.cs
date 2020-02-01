using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.DDDAPISample.Domain.ForeignExchange;

namespace TotalNetCore.DDDAPISample.Infrastructure.Domain.ForeignExchange
{
    public class ConversionRatesCache
    {
        public List<ConversionRate> Rates { get; }

        public ConversionRatesCache(List<ConversionRate> rates)
        {
            this.Rates = rates;
        }
    }
}

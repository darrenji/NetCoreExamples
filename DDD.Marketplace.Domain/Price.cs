using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Marketplace.Domain
{
    public class Price : Money
    {
        public Price(decimal amount, string currencyCode, ICurrencyLookup currencyLookup):base(amount, currencyCode, currencyLookup)
        {
            if(amount < 0)
            {
                throw new ArgumentException("Price cannot be negative", nameof(amount));
            }
        }
    }
}

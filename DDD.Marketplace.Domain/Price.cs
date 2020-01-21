using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Marketplace.Domain
{
    public class Price : Money
    {
        private Price(decimal amount, string currencyCode, ICurrencyLookup currencyLookup) : base(amount, currencyCode, currencyLookup)
        {
            if(amount < 0)
            {
                throw new ArgumentException("Price cannot be negative", nameof(amount));
            }
        }

        public static new Price FromDecimal(decimal amount, string currency, ICurrencyLookup currencyLookup)
        {
            return new Price(amount, currency, currencyLookup); 
        }

       
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.DDDAPISample.Domain.Shared;

namespace TotalNetCore.DDDAPISample.Domain.ForeignExchange
{
    public class ConversionRate
    {
        public string SourceCurrency { get; }
        public string TargetCurrency { get; }
        public decimal Factor { get; }

        public ConversionRate(string sourceCurrency, string targetCurrency, decimal factor)
        {
            this.SourceCurrency = sourceCurrency;
            this.TargetCurrency = targetCurrency;
            this.Factor = factor;
        }

        internal Money Convert(Money value)
        {
            return new Money(value.Value * this.Factor, this.TargetCurrency);
        }
    }
}

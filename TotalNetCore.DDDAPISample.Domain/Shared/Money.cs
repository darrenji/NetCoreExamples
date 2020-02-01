using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.DDDAPISample.Domain.Shared
{
    public class Money
    {
        public decimal Value { get; }
        public string Currency { get; }

        public Money(decimal value, string currency)
        {
            this.Value = value;
            this.Currency = currency;
        }
    }
}

using DDD.Marketplace.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDD.Marketplace.Tests
{
    public class FakeCurrencyLookup : ICurrencyLookup
    {
        private static readonly IEnumerable<CurrencyDetails> _currencies = new[] { 
            new CurrencyDetails{ CurrencyCode="EUR", DecimalPlaces=2, InUse=true},
            new CurrencyDetails{ CurrencyCode="USD", DecimalPlaces=2, InUse=true},
            new CurrencyDetails{CurrencyCode="JPY", DecimalPlaces=0, InUse=true},
            new CurrencyDetails{CurrencyCode="DEM", DecimalPlaces=2, InUse=false}
        };
        public CurrencyDetails FindCurrency(string currencyCode)
        {
            var currency = _currencies.FirstOrDefault(t => t.CurrencyCode == currencyCode);
            return currency ?? CurrencyDetails.None;//这里隐含的技巧是，当对象为NULL的时候不返回Null,而返回一个实例，这个实例中的某个属性表示对象不存在
        }
    }
}

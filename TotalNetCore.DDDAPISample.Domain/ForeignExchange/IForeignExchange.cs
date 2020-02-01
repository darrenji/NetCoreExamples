using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.DDDAPISample.Domain.ForeignExchange
{
    public interface IForeignExchange
    {
        List<ConversionRate> GetConversionRates();
    }
}

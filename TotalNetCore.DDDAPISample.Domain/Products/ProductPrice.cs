using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.DDDAPISample.Domain.Shared;

namespace TotalNetCore.DDDAPISample.Domain.Products
{
    public class ProductPrice
    {
        public Money Value { get; private set; }

        private ProductPrice() { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TotalNetCore.DDDAPISample.Domain.Interfaces;
using TotalNetCore.DDDAPISample.Domain.Shared;

namespace TotalNetCore.DDDAPISample.Domain.Products
{
   public  class Product:Entity,IAggregateRoot
    {
        public ProductId Id { get; private set; }
        public string Name { get; private set; }

        private List<ProductPrice> _prices;

        private Product() { }

        internal Money GetPrice(string currency)
        {
            return this._prices.Single(t => t.Value.Currency == currency).Value;
        }
    }
}

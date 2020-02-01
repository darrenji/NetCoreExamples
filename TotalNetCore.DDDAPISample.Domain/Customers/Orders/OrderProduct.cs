using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TotalNetCore.DDDAPISample.Domain.ForeignExchange;
using TotalNetCore.DDDAPISample.Domain.Products;
using TotalNetCore.DDDAPISample.Domain.Shared;

namespace TotalNetCore.DDDAPISample.Domain.Customers.Orders
{
    public class OrderProduct : Entity
    {
        public int Quantity { get; private set; }

        public ProductId ProductId { get; private set; }

        internal Money Value { get; private set; }
        internal Money ValueInEUR { get; private set; }

        private OrderProduct() { }//私有无参构造函数

        private OrderProduct(Product product, int quantity, string currency, List<ConversionRate> conversionRates)//私有多参构造函数，供本领域使用
        {
            this.ProductId = product.Id;
            this.Quantity = quantity;

            //本质上是给Value, ValueInEUR赋值
            this.CalculateValue(product, currency, conversionRates);
        }
        
        /// <summary>
        /// 工厂方法
        /// </summary>
        internal static OrderProduct CreateForProduct(Product product, int quantity, string currency, List<ConversionRate> conversionRates)
        {
            return new OrderProduct(product, quantity, currency, conversionRates);
        }

        //改变数量
        internal void ChangeQuantity(Product product, int quantity, List<ConversionRate> conversionRates)
        {
            this.Quantity = Quantity;
            this.CalculateValue(product, this.Value.Currency, conversionRates);
        }

        //私有方法
        private void CalculateValue(Product product, string currency, List<ConversionRate> conversionRates)
        {
            var totalValueForOrderProduct = this.Quantity * product.GetPrice(currency).Value;
            this.Value = new Money(totalValueForOrderProduct, currency);

            var conversionRate = conversionRates.Single(x => x.SourceCurrency == currency && x.TargetCurrency == "EUR");

            this.ValueInEUR = conversionRate.Convert(this.Value);
        }
    }
}

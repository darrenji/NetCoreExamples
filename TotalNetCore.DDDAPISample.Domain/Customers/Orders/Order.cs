using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TotalNetCore.DDDAPISample.Domain.ForeignExchange;
using TotalNetCore.DDDAPISample.Domain.Products;
using TotalNetCore.DDDAPISample.Domain.Shared;

namespace TotalNetCore.DDDAPISample.Domain.Customers.Orders
{
    public class Order : Entity
    {
        internal OrderId Id;//订单编号
        private bool _isRemoved;//订单是否被删除
        private Money _value;//本国价格
        private Money _valueInEUR;//欧洲价格
        private List<OrderProduct> _orderProducts;//这里体现聚合，所有的订单产品
        private OrderStatus _status;//订单状态
        private DateTime _orderDate;//订单创建日期
        private DateTime? _orderChangeDate;//订单改变日期

        private Order()//私有构造函数初始化
        {
            this._orderProducts = new List<OrderProduct>();
            this._isRemoved = false;
        }

        private Order(//多参数私有构造函数，供本领域内部使用
            List<OrderProductData> orderProductsData,
            List<Product> allProducts,
            string currency, 
            List<ConversionRate> conversionRates
            )
        {
            this._orderDate = DateTime.UtcNow;
            this.Id = new OrderId(Guid.NewGuid());
            this._orderProducts = new List<OrderProduct>();

            foreach(var orderProductData in orderProductsData)
            {
                var product = allProducts.Single(t => t.Id == orderProductData.ProductId);

                //把订单产品的事交给OrderProduct
                var orderProduct = OrderProduct.CreateForProduct(product, orderProductData.Quantity, currency, conversionRates);
                _orderProducts.Add(orderProduct);
            }

            this.CalculateOrderValue();
            this._status = OrderStatus.Placed;

        }

        //订单创建
        internal static Order CreateNew(List<OrderProductData> orderProductsData,
           List<Product> allProducts,
           string currency,
           List<ConversionRate> conversionRates)
        {
            return new Order(orderProductsData, allProducts, currency, conversionRates);
        }

        //订单改变
        internal void Change(
            List<Product> allProducts,
            List<OrderProductData> orderProductsData,
            List<ConversionRate> conversionRates,
            string currency)
        {
            foreach (var orderProductData in orderProductsData)
            {
                var product = allProducts.Single(x => x.Id == orderProductData.ProductId);

                var existingProductOrder = _orderProducts.SingleOrDefault(x => x.ProductId == orderProductData.ProductId);
                if (existingProductOrder != null)
                {
                    var existingOrderProduct = this._orderProducts.Single(x => x.ProductId == existingProductOrder.ProductId);

                    existingOrderProduct.ChangeQuantity(product, orderProductData.Quantity, conversionRates);
                }
                else
                {
                    var orderProduct = OrderProduct.CreateForProduct(product, orderProductData.Quantity, currency, conversionRates);
                    this._orderProducts.Add(orderProduct);
                }
            }

            var orderProductsToCheck = _orderProducts.ToList();
            foreach (var existingProduct in orderProductsToCheck)
            {
                var product = orderProductsData.SingleOrDefault(x => x.ProductId == existingProduct.ProductId);
                if (product == null)
                {
                    this._orderProducts.Remove(existingProduct);
                }
            }

            this.CalculateOrderValue();

            this._orderChangeDate = DateTime.UtcNow;
        }

        //订单移除
        internal void Remove()
        {
            this._isRemoved = true;
        }

        //订单，是否是今天的订单
        internal bool IsOrderedToday()
        {
            return this._orderDate.Date == DateTime.UtcNow.Date;

        }

        //私有方法
        private void CalculateOrderValue()
        {
            var value = this._orderProducts.Sum(x => x.Value.Value);
            this._value = new Money(value, this._orderProducts.First().Value.Currency);

            var valueInEUR = this._orderProducts.Sum(x => x.ValueInEUR.Value);
            this._valueInEUR = new Money(valueInEUR, "EUR");
        }
    }
}

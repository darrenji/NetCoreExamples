using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TotalNetCore.DDDAPISample.Domain.Customers.Orders;
using TotalNetCore.DDDAPISample.Domain.ForeignExchange;
using TotalNetCore.DDDAPISample.Domain.Interfaces;
using TotalNetCore.DDDAPISample.Domain.Products;
using TotalNetCore.DDDAPISample.Domain.Shared;

namespace TotalNetCore.DDDAPISample.Domain.Customers
{
    public class Customer : Entity, IAggregateRoot
    {
        public CustomerId Id { get; private set; }//客户的主键
        public string _email;//客户的邮件
        public string _name;//客户的姓名

        private readonly List<Order> _orders;//这里体现聚合，客户的订单
        private bool _welcomeEmailWasSent;//是否给客户发送欢迎邮件

        private Customer()//每一个领域都有一个私有构造函数
        {
            this._orders = new List<Order>();
        }

        private Customer(string email, string name)//私有构造函数的重载
        {
            //设置领域的状态或属性
            this.Id = new CustomerId(Guid.NewGuid());
            _email = email;
            _name = name;
            _welcomeEmailWasSent = false;

            //把事件通知出去
            this.AddDomainEvent(new CustomerEvents.CustomerRegisteredEvent(this.Id));
        }

        //客户创建
        public static Customer CreateRegistered(string email, string name, ICustomerUniquenessChecker customerUniquenessChecker)
        {
            var isUnique = customerUniquenessChecker.IsUnique(email);
            if(!isUnique)
            {
                throw new BusinessRuleValidationException("Customer with this email already existes.");
            }

            return new Customer(email, name);
        }

        //客户下单
        public void PlaceOrder(
                List<OrderProductData> orderProductDatas,
                List<Product> allProducts,
                string currency,
                List<ConversionRate> conversionRates)
        {
            if (this._orders.Count(x => x.IsOrderedToday()) >= 2)
            {
                throw new BusinessRuleValidationException("You cannot order more than 2 orders on the same day");
            }

            //把订单的事情交给Order
            var order = Order.CreateNew(orderProductDatas, allProducts, currency, conversionRates);
            this._orders.Add(order);

            this.AddDomainEvent(new OrderEvents.OrderPlacedEvent(order.Id));
        }

        //客户更改订单
        public void ChangeOrder(
            OrderId orderId,
            List<Product> existingProducts,
            List<OrderProductData> newOrderProductsData,
            List<ConversionRate> conversionRates,
            string currency)
        {
            var order = this._orders.Single(x => x.Id == orderId);
            order.Change(existingProducts, newOrderProductsData, conversionRates, currency);

            this.AddDomainEvent(new OrderEvents.OrderChangedEvent(orderId));
        }

        //客户移除订单
        public void RemoveOrder(OrderId orderId)
        {
            var order = this._orders.Single(x => x.Id == orderId);
            order.Remove();

            this.AddDomainEvent(new OrderEvents.OrderRemovedEvent(orderId));
        }

        //客户已经发送欢迎邮件
        public void MarkAsWelcomedByEmail()
        {
            this._welcomeEmailWasSent = true;
        }
    }
}

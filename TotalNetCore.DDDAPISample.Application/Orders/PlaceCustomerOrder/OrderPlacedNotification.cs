using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.DDDAPISample.Application.Configuration.DomainEvents;
using TotalNetCore.DDDAPISample.Domain.Customers.Orders;


namespace TotalNetCore.DDDAPISample.Application.Orders.PlaceCustomerOrder
{
    public class OrderPlacedNotification : DomainNotificationBase<TotalNetCore.DDDAPISample.Domain.Customers.Orders.OrderEvents.OrderPlacedEvent>
    {
        public OrderId OrderId { get; }

        public OrderPlacedNotification(TotalNetCore.DDDAPISample.Domain.Customers.Orders.OrderEvents.OrderPlacedEvent domainEvent) : base(domainEvent)
        {
            this.OrderId = domainEvent.OrderId;
        }

        [JsonConstructor]
        public OrderPlacedNotification(OrderId orderId) : base(null)
        {
            this.OrderId = orderId;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.DDDAPISample.Domain.Shared;

namespace TotalNetCore.DDDAPISample.Domain.Customers.Orders
{
    public class OrderEvents
    {
        public class OrderPlacedEvent : DomainEventBase
        {
            public OrderId OrderId { get; }
            public OrderPlacedEvent(OrderId orderId)
            {
                OrderId = orderId;
            }
        }

        public class OrderChangedEvent : DomainEventBase
        {
            public OrderId OrderId { get; }

            public OrderChangedEvent(OrderId orderId)
            {
                this.OrderId = orderId;
            }
        }

        public class OrderRemovedEvent : DomainEventBase
        {
            public OrderId OrderId { get; }

            public OrderRemovedEvent(OrderId orderId)
            {
                this.OrderId = orderId;
            }
        }
    }
}

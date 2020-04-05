using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.Micro.Domain.OrderAggregate;
using TotalNetCore.Micro.DomainAbstraction;

namespace TotalNetCore.Micro.Domain.Events
{
    public class OrderCreatedDomainEvent : IDomainEvent
    {
        public Order Order { get; private set; }
        public OrderCreatedDomainEvent(Order order)
        {
            this.Order = order;
        }
    }
}

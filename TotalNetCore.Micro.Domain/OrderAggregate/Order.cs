using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.Micro.Domain.Events;
using TotalNetCore.Micro.DomainAbstraction;

namespace TotalNetCore.Micro.Domain.OrderAggregate
{
    public class Order : Entity<long>, IAggregateRoot
    {
        public string UserId { get; private set; }
        public string UserName { get; private set; }
        public Address Address { get; private set; }
        public int ItemCount { get; private set; }

        protected Order() { }

        public Order(string userId, string userName, int itemCount, Address address)
        {
            this.UserId = UserId;
            this.UserName = userName;
            this.Address = address;
            this.ItemCount = itemCount;

            this.AddDomainEvent(new OrderCreatedDomainEvent(this));
        }

        public void ChangeAddress(Address address)
        {
            this.Address = address;
        }
    }
}

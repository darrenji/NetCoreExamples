using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.DDDAPISample.Domain.Shared;

namespace TotalNetCore.DDDAPISample.Domain.Customers
{
    public class CustomerEvents
    {
        public class CustomerRegisteredEvent : DomainEventBase
        {
            public CustomerId CustomerId { get; }

            public CustomerRegisteredEvent(CustomerId customerId)
            {
                this.CustomerId = customerId;
            }
        }
    }
}

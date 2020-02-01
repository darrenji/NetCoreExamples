using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.DDDAPISample.Application.Configuration.DomainEvents;
using TotalNetCore.DDDAPISample.Domain.Customers;
using static TotalNetCore.DDDAPISample.Domain.Customers.CustomerEvents;

namespace TotalNetCore.DDDAPISample.Application.Customers.IntegrationHandlers
{
    /// <summary>
    /// 有关Customer的Domain Ntofication Object
    /// </summary>
    public class CustomerRegisteredNotification : DomainNotificationBase<CustomerRegisteredEvent>
    {
        public CustomerId CustomerId { get; }

        public CustomerRegisteredNotification(CustomerRegisteredEvent domainEvent) : base(domainEvent)
        {
            this.CustomerId = domainEvent.CustomerId;
        }

        [JsonConstructor]
        public CustomerRegisteredNotification(CustomerId customerId) : base(null)
        {
            this.CustomerId = customerId;
        }
    }
}

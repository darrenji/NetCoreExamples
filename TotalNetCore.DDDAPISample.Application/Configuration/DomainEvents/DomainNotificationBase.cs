using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.DDDAPISample.Application.Configuration.DomainEvents
{
    /// <summary>
    /// Domain Notification Object的实现
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DomainNotificationBase<T> : IDomainEventNotification<T>, INotification
    {
        [JsonIgnore]
        public T DomainEvent { get; }

        public DomainNotificationBase(T domainEvent)
        {
            this.DomainEvent = domainEvent;
        }
    }
}

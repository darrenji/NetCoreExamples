using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.DDDAPISample.Application.Configuration.DomainEvents
{
    /// <summary>
    /// 领域Notification Object,也是Outbox Pattern的写法之一
    /// </summary>
    /// <typeparam name="TEventType"></typeparam>
    public interface IDomainEventNotification<out TEventType>
    {
        TEventType DomainEvent { get; }
    }
}

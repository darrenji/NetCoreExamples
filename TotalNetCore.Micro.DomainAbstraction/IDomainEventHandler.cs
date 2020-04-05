using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.Micro.DomainAbstraction
{
    public interface IDomainEventHandler<TDomainEvent> : INotificationHandler<TDomainEvent> where TDomainEvent : IDomainEvent
    {
    }
}

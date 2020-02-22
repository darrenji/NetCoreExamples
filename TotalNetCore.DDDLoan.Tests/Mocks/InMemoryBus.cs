using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TotalNetCore.DDDLoan.Web.DomainModel.Ddd;

namespace TotalNetCore.DDDLoan.Tests.Mocks
{
    public class InMemoryBus : IEventPublisher
    {
        private readonly List<DomainEvent> events = new List<DomainEvent>();
        public void Publish(DomainEvent @event)
        {
            events.Add(@event);
        }

        public ReadOnlyCollection<DomainEvent> Events => events.AsReadOnly();
    }
}

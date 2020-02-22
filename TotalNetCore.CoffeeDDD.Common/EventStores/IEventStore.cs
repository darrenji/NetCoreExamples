using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.CoffeeDDD.Common
{
    public interface IEventStore
    {
        List<Event> GetEventsForAggregate(Guid aggregateId);
        void SaveEvents(Guid aggregateId, IEnumerable<Event> events, int expectedVersion);
    }
}

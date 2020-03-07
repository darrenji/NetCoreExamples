using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.DDDCafe.Lib.CQRS
{
    public interface IEventStore
    {
        List<Event> LoadEventsFor<TAggregate>(Guid id);
        void SaveEventsFor<TAggregate>(Guid id, int eventsLoaed, List<Event> newEvents);
    }
}

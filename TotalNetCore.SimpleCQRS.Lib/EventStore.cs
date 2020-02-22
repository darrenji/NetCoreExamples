using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TotalNetCore.SimpleCQRS.Lib
{
    /// <summary>
    /// 管理所有Aggregate的事件，借助IEventPublisher发布出去
    /// </summary>
    public class EventStore : IEventStore
    {
        private readonly IEventPublisher _publisher;

        public EventStore(IEventPublisher publisher)
        {
            _publisher = publisher;
        }

        private readonly Dictionary<Guid, List<EventDescriptor>> _current = new Dictionary<Guid, List<EventDescriptor>>();

        public List<Event> GetEventsForAggregate(Guid aggregateId)
        {
            List<EventDescriptor> eventDescriptors;

            if(!_current.TryGetValue(aggregateId, out eventDescriptors))
            {
                throw new AggregateNotFoundException();
            }

            return eventDescriptors.Select(t => t.EventData).ToList();
        }

        public void SaveEvents(Guid aggregateId, IEnumerable<Event> events, int expectedVersion)
        {
            List<EventDescriptor> eventDescriptors;

            //先获取，如果没有添加
            if (!_current.TryGetValue(aggregateId, out eventDescriptors))
            {
                eventDescriptors = new List<EventDescriptor>();
                _current.Add(aggregateId, eventDescriptors);
            }
            else if (eventDescriptors[eventDescriptors.Count - 1].Version != expectedVersion && expectedVersion!=-1)
            {
                throw new ConcurrencyException();
            }

            var i = expectedVersion;

            foreach(var @event in events)
            {
                i++;
                @event.Version = i;

                eventDescriptors.Add(new EventDescriptor(aggregateId, @event, i));

                _publisher.Publish(@event);
            }
        }

        private struct EventDescriptor
        {
            public readonly Event EventData;
            public readonly Guid Id;
            public readonly int Version;

            public EventDescriptor(Guid id, Event eventData, int version)
            {
                EventData = eventData;
                Version = version;
                Id = id;
            }

        }
    }
}

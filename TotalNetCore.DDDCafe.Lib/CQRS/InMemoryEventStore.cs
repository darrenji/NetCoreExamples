using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TotalNetCore.DDDCafe.Lib.CQRS
{
    public class InMemoryEventStore : IEventStore
    {
        private class Stream
        {
            public List<Event> Events;
        }

        private ConcurrentDictionary<Guid, Stream> store = new ConcurrentDictionary<Guid, Stream>();

        public List<Event> LoadEventsFor<TAggregate>(Guid id)
        {
            Stream s;

            if(store.TryGetValue(id, out s))
            {
                return s.Events;
            }
            else
            {
                return new List<Event>();
            }
        }

        public void SaveEventsFor<TAggregate>(Guid id, int eventsLoaed, List<Event> newEvents)
        {
            //获取某个Aggregate的事件对象
            var s = store.GetOrAdd(id, _ => new Stream());

            while(true)
            {
                //获取事件对象的所有事件
                var eventList = s.Events;

                //数据库中统计的、已经发生的事件
                var preEventsCount = eventList == null ? 0 : eventList.Count;

                if (preEventsCount != eventsLoaed)//数据库中记录的事件数量和方法传入的事件数量必须相等
                    throw new Exception("Concurrency conflict; cannot persist these events.");

                //新的所有事件
                var newEventList = eventList == null ? new List<Event>() : eventList;
                newEventList.AddRange(newEvents);

                //结果是把newEvents赋值给s.Events
                if(Interlocked.CompareExchange(ref s.Events, newEvents, eventList)==eventList)
                {
                    break;
                }
            }
        }
    }
}

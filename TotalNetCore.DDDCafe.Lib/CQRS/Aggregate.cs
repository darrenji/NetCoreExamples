using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.DDDCafe.Lib.CQRS
{
   public  class Aggregate
    {
        public int EventsLoaded { get; private set; }
        public Guid Id { get; internal set; }

        public void ApplyEvents(List<Event> events)
        {
            foreach(var e in events)
            {
                GetType().GetMethod("ApplyOneEvent")
                    .MakeGenericMethod(e.GetType())
                    .Invoke(this, new object[] { e });
            }
        }

        public void ApplyOneEvent<TEvent>(TEvent e) where TEvent:Event
        {
            var applier = this as IApplyEvent<TEvent>;
            if (applier == null)
                throw new InvalidOperationException($"Aggregate {GetType().Name} does not know how to apply event {e.GetType().Name}");
            applier.Apply(e);
            EventsLoaded++;
        }
    }
}

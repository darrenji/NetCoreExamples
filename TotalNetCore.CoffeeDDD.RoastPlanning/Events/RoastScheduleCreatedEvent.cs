using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.CoffeeDDD.Common;

namespace TotalNetCore.CoffeeDDD.RoastPlanning.Events
{
    public class RoastScheduleCreatedEvent : Event
    {
        public readonly Guid RoastScheduleId;
        public readonly DateTime RoastWeekStartsOn;

        public RoastScheduleCreatedEvent(Guid roastScheduleId, DateTime roastWeekStartsOn)
        {
            RoastScheduleId = roastScheduleId;
            RoastWeekStartsOn = roastWeekStartsOn;
        }
    }
}

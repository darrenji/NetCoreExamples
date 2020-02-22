using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.CoffeeDDD.Common;

namespace TotalNetCore.CoffeeDDD.RoastPlanning.Events
{
   public  class RoastScheduleRoastDayChosenEvent : Event
    {
        public Guid RoastScheduleId { get; private set; }
        public DayOfWeek[] Days { get; private set; }

        public RoastScheduleRoastDayChosenEvent(Guid roastScheduleId, DayOfWeek[] days)
        {
            RoastScheduleId = roastScheduleId;
            Days = days;
        }
    }
}

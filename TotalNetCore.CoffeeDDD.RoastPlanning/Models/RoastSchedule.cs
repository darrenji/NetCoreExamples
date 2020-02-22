using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TotalNetCore.CoffeeDDD.Common;
using TotalNetCore.CoffeeDDD.RoastPlanning.Events;

namespace TotalNetCore.CoffeeDDD.RoastPlanning.Models
{
    public class RoastSchedule : AggregateRoot
    {
        public DateTime RoastWeekStartsOn { get; private set; }
        public RoastDays RoastDays { get; private set; }

        //本质是更改Aggregate的状态
        public void Apply(RoastScheduleCreatedEvent e)
        {
            RoastWeekStartsOn = e.RoastWeekStartsOn;
            Id = e.RoastScheduleId;
        }

        //
        public void Apply(RoastScheduleRoastDayChosenEvent e)
        {
            RoastDays = new RoastDays(e.Days);
        }

        public RoastSchedule(Guid id, DateTime roastWeekStartsOn)
        {
            RoastDays = new RoastDays(new HashSet<RoastDay>());

            //不仅把event放到集合中，还调用本类的Apply方法
            ApplyChange(new RoastScheduleCreatedEvent(id, roastWeekStartsOn));
        }

        public void SetRoastDays(RoastDays roastDays)
        {
            if(roastDays.Days.Count==0)
            {
                throw new ArgumentNullException("roastDays count must be greater than 0");
            }
            var newDays = roastDays.Days.Select(t => t.Day).ToArray();
            ApplyChange(new RoastScheduleRoastDayChosenEvent(Id, newDays));
        }

        public RoastSchedule()
        {

        }
    }
}

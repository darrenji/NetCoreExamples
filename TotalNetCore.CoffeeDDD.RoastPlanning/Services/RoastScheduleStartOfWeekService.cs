using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.CoffeeDDD.RoastPlanning.Services
{
   public static class RoastScheduleStartOfWeekService
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = dt.DayOfWeek - startOfWeek;
            if(diff<0)
            {
                diff += 7;
            }
            return dt.AddDays(-1 * diff).Date;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.CoffeeDDD.Common;

namespace TotalNetCore.CoffeeDDD.RoastPlanning.Commands
{
    public class ChooseRoastDaysForRoastScheduleCommand : Command
    {
        public readonly DayOfWeek[] RoastDays;
        public readonly int OriginalVersion;

        public ChooseRoastDaysForRoastScheduleCommand(Guid id, DayOfWeek[] roastDays, int originalVersion)
        {
            RoastDays = roastDays;
            OriginalVersion = originalVersion;
        }
    }
}

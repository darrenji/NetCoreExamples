using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.CoffeeDDD.Common;

namespace TotalNetCore.CoffeeDDD.RoastPlanning.Commands
{
   public  class CreateNewRoastScheduleCommand : Command
    {
        public readonly Guid RoastScheduleId;

        public CreateNewRoastScheduleCommand(Guid roastScheduleId)
        {
            RoastScheduleId = roastScheduleId;
        }
    }
}

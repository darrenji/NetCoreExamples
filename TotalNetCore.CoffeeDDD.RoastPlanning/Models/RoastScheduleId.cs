using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.CoffeeDDD.Common.Models;

namespace TotalNetCore.CoffeeDDD.RoastPlanning.Models
{
    public class RoastScheduleId : Identity
    {
        public RoastScheduleId():base()
        {

        }

        public RoastScheduleId(string id) :base(id)
        {

        }
    }
}

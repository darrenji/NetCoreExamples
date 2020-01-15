using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.FactoryPattern
{
    public class CoolingFactory : AirConditionerFactory
    {
        public override IAirConditioner Create(double temperature)
        {
            return new Cooling(temperature);
        }
    }
}

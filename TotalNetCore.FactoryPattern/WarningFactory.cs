using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.FactoryPattern
{
    public class WarningFactory : AirConditionerFactory
    {
        public override IAirConditioner Create(double temperature)
        {
            return new Warning(temperature);
        }
    }
}

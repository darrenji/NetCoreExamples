using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.FactoryPattern
{
    public class AirConditioner
    {
        private readonly Dictionary<Actions, AirConditionerFactory> _factories;

        public AirConditioner()
        {
            _factories = new Dictionary<Actions, AirConditionerFactory> {
                { Actions.Cooling, new CoolingFactory()},
                { Actions.Warning, new WarningFactory()}
            };
        }
    }
}

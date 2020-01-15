using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.FactoryPattern
{
    public class Cooling : IAirConditioner
    {
        private readonly double _temperature;

        public Cooling(double temperature)
        {
            _temperature = temperature;
        }
        public void Operate()
        {
            Console.WriteLine($"降温到：{_temperature}");
        }
    }
}

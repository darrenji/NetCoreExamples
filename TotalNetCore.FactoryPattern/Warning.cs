using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.FactoryPattern
{
    public class Warning : IAirConditioner
    {
        private readonly double _temperature;

        public Warning(double temperature)
        {
            _temperature = temperature;
        }
        public void Operate()
        {
            Console.WriteLine($"升温到：{_temperature}");
        }
    }
}

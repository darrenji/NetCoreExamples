using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.FactoryPattern
{
    /// <summary>
    /// 把所有工厂类型放到键值对，通过反射获取工厂
    /// </summary>
    public class AirConditioner1
    {
        private readonly Dictionary<Actions, AirConditionerFactory> _factories;

        public AirConditioner1()
        {
            _factories = new Dictionary<Actions, AirConditionerFactory>();

            foreach(Actions action in Enum.GetValues(typeof(Actions)))
            {
                var factory = (AirConditionerFactory)Activator.CreateInstance(Type.GetType("TotalNetCore.FactoryPattern." + Enum.GetName(typeof(Actions), action) + "Factory"));
                _factories.Add(action, factory);
            }
        }

        public IAirConditioner ExecuteCreation(Actions action, double temperature) => _factories[action].Create(temperature);
    }
}

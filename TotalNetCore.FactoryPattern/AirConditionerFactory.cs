using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.FactoryPattern
{
    //抽象工厂
    public abstract class AirConditionerFactory
    {
        //产生接口
        public abstract IAirConditioner Create(double temperature);
    }
}

using System;

namespace TotalNetCore.FactoryPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            //所谓工厂就是通过不同的条件生成某个接口的不同实现类
            //通过抽象工厂来实现
            //把所有工厂放在键值对中，是很好的方式
            var factory = new AirConditioner1().ExecuteCreation(Actions.Cooling, 2.5);
            factory.Operate();
        }
    }
}

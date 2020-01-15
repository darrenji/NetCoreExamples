using System;

namespace TotalNetCore.FacadeBuilderPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            //把一个复杂对象用多个Buidler构建
            //最有有一个Bulder的管理类，里面包含所有的Builder
            //把所有的构建者放在基类中

            var car = new CarBuilderFacade()
                .Info
                .WithType("dazhong")
                .WithColor("red")
                .WithNumberOfDoors(4)
                .Address
                .InCity("nantong")
                .AtAddress("rudong")
                .Build();

            Console.WriteLine(car);

        }
    }
}

using System;

namespace TotalNetCore.FluentBuilderWithRecursive
{
    class Program
    {
        static void Main(string[] args)
        {
            var employeeBuilder = new EmployeeInfoBuilder();

            //AtPostion方法无法调用，因为SetName返回的是EmployeeInfoBuilder,而AtPosition是EmployeeInfoBuilder的子类EmployeePositionBuilder的方法
            //如果想把子类的行为加到父类上呢？--recursive generics
            //employeeBuilder.SetName("").AtPosition();

            var emp = NewEmployeeBuilderDirector
                .NewEmployee //抽象类EmployeeBuilder自己不能实例化，但可以有构造函数，通过子类来实例化抽象类，从而得到抽象类的一个单例
                .SetName("")
                .AtPosition("")
                .WithSalary(20)
                .Build();

            Console.WriteLine(emp);

            //通过泛型让父类知道子类，父类调用子类方法的时候，由于是泛型，所以返回的其实是子类
            //似乎适用于通过不同的子类来构造一个对象
        }
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotalNetCore.StartupDemo.Extensions
{
    public class RequestSetOptionsStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            //这里估计就是装饰器设计模式的体现
            return builder => {
                builder.UseMiddleware<RequestSetOptionsMiddleware>();
                next(builder);
            };
        }
    }

    public class Person
    {
        public Person()
        {

        }

        private string name;

        public Person(string name)
        {
            this.name = name;
        }

        public virtual void Show()
        {
            Console.WriteLine($"我的名字是：{name}");
        }
    }

    public class Finery : Person
    {
        protected Person component;//父类作为子类的字段

        /// <summary>
        /// 子类相当于外层，给子类的基类提供一个方法，用来设置其内层
        /// </summary>
        /// <param name="component"></param>
        public void Decorate(Person component)
        {
            this.component = component;
        }

        //子类和父类有相同的行为
        public override void Show()
        {
            if(component!=null)
            {
                component.Show();
            }
        }
    }

    public class TShirts : Finery
    {
        public override void Show()
        {
            Console.WriteLine("大T恤");
            base.Show();
        }
    }

    public class BigTrouser : Finery
    {
        public override void Show()
        {
            Console.WriteLine("垮裤");
            base.Show();
        }
    }
}

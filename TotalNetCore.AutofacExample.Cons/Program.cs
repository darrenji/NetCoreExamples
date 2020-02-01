using Autofac;
using System;
using System.ComponentModel;
using TotalNetCore.AutofacExample.Lib;

namespace TotalNetCore.AutofacExample.Cons
{
    class Program
    {
        private static Autofac.IContainer Container { get; set; }
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleOutput>().As<IOutput>();
            builder.RegisterType<TodayWriter>().As<IDateWriter>();
            Container = builder.Build();

            WriteDate();
        }

        public static void WriteDate()
        {
            using(var scope = Container.BeginLifetimeScope())//scope的意思是当IContainer被dispose,里面的component都被dispose.像这种手动写using的方式不多
            {
                var writer = scope.Resolve<IDateWriter>();
                writer.WriteDate();
            }
        }
    }
}

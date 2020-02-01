using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TotalNetCore.AutofacExample.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .UseServiceProviderFactory(new AutofacServiceProviderFactory())
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder
        //                .UseContentRoot(Directory.GetCurrentDirectory())
        //                .UseIISIntegration()
        //                .UseStartup<Startup>();
        //        });

        public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
            //这样就可以在Startup中使用ConfigureContainer方法了，并且使用一个强类型ContainerBuilder
            //如果不这样，在Startup中就无法使用ConfigureContainer了
            //调用Populate之后，在ConfigureServices中注册的方法就被注册到了Autofac的容器中了
        .ConfigureServices(services => services.AddAutofac())
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder
                .UseStartup<Startup>();
        });
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using TotalNetCore.StartupDemo.Extensions;

namespace TotalNetCore.StartupDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder方法名不要轻易更改，Entity Framework Core的Tools希望用到这个方法，在APP运行起来之前使用一些配置
            var host = CreateHostBuilder(args).Build();

            Person p = new Person("最内层");
            var t = new TShirts();
            var big = new BigTrouser();
            big.Decorate(t);
            t.Decorate(p);
            big.Show();

            host.Run();

        }

        #region 默认的方式
        public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)//CreateDefaultBuilder创建了一个叫做Build的对象，这种对象可以进行链式
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();//在这里显式使用Startup
        })
        .ConfigureServices(services=> {
            services.AddTransient<IStartupFilter, RequestSetOptionsStartupFilter>();
        });

        #endregion

        #region 启动顺序 ConfigureHostConfiguration→ConfigureAppConfiguration→ConfigureService→Startup.ConfigureServices→Configure
        //    public static IHostBuilder CreateHostBuilder(string[] args) =>
        //Host.CreateDefaultBuilder(args)//CreateDefaultBuilder创建了一个叫做Build的对象，这种对象可以进行链式
        //    .ConfigureServices(context =>
        //    {
        //        Console.WriteLine("ConfigureServices");
        //    })
        //     .ConfigureAppConfiguration((context, builder) =>
        //     {
        //         Console.WriteLine("ConfigureAppConfiguration");
        //     })
        //    .ConfigureHostConfiguration(builder =>
        //    {
        //        Console.WriteLine("ConfigureHostConfiguration");
        //    })
        //    .ConfigureWebHostDefaults(webBuilder =>
        //    {
        //        webBuilder.UseStartup<Startup>();
        //    }); 
        #endregion

        #region 这种方式无法启动网站
        //        public static IHostBuilder CreateHostBuilder(string[] args) =>
        //Host.CreateDefaultBuilder(args); 
        #endregion

        #region 不使用Startup
        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureAppConfiguration((hostingContext, config) => { })
        //        .ConfigureWebHostDefaults(webBuilder => {
        //            webBuilder.ConfigureServices(services =>
        //            {
        //                Console.WriteLine("Startup.ConfigureServices");
        //                services.AddRazorPages();
        //            })
        //            .Configure(app => {
        //                Console.WriteLine("Startup.Configure");

        //                //每一个Use开头的方法就是在请求管道里增加一个中间件
        //                //中间件还必须有一个功能就是Invoke下一个中间件
        //                app.UseStaticFiles();

        //                app.UseRouting();

        //                app.UseAuthorization();

        //                app.UseEndpoints(endpoints =>
        //                {
        //                    endpoints.MapRazorPages();
        //                });
        //            });
        //        });
        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TotalNetCore.ByMrXiao.启动过程
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(builder => {
                    Console.WriteLine("ConfigureAppConfiguration");
                })
                .ConfigureServices(services => {
                    Console.WriteLine("ConfigureServices");
                })
                .ConfigureHostConfiguration(builder => {
                    Console.WriteLine("ConfigureHostConfiguration");
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    Console.WriteLine("ConfigureWebHostDefaults");
                    //webBuilder.UseStartup<Startup>();

                    //Startup的设置放在这里
                    webBuilder.ConfigureServices(services => { });
                    webBuilder.Configure(builder=> { });
                });

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });
    }
}

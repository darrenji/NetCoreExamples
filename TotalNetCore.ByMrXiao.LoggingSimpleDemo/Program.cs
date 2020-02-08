using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace TotalNetCore.ByMrXiao.LoggingSimpleDemo
{
    class Program
    {
       

        static void Main(string[] args)
        {
            //IConfigurationBuilder用来创建IConfiguratioin
            IConfigurationBuilder configBuilder = new ConfigurationBuilder();
 
            configBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange:true);
            var config = configBuilder.Build();

            //IServiceCollection用来注册服务
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IConfiguration>(t=>config);//工厂方式把配置放到容器里,IConfiguration被注入到容器

            //AddLogging内含一些服务注册
            //AddLogging是IServiceCollection的一个扩展方法
            //把ILoggerFactory和LoggerFactory加入容器
            //把ILogger和Logger放入容器
            serviceCollection.AddLogging(builder => {
                builder.AddConfiguration(config.GetSection("Logging"));
                builder.AddConsole();
                builder.AddDebug();
            });

            //IServiceProvider看作是容器
            IServiceProvider service = serviceCollection.BuildServiceProvider();

            var logger = service.GetService<ILogger<Program>>();


            while(Console.ReadKey().Key!=ConsoleKey.Escape)
            {
                using (logger.BeginScope("ScopeId:{scopeId}", Guid.NewGuid()))
                {
                    logger.LogInformation("这是Info");
                    logger.LogError("this is error");
                    logger.LogTrace("this is trace");
                }
                System.Threading.Thread.Sleep(100);
                Console.WriteLine("==分隔线==");
            }
            

            //serviceCollection.AddTransient<OrderService>();



            //var order = service.GetService<OrderService>();
            //order.Show();

            ////ILoggerFactory用来生产ILogger
            //ILoggerFactory loggerFactory = service.GetService<ILoggerFactory>();

            //var aLogger = loggerFactory.CreateLogger("aLogger");
            //aLogger.LogDebug(2001, "a");

            //var bLogger = loggerFactory.CreateLogger("bLogger");
            //bLogger.LogDebug("b");

            //var logger = service.GetService<ILogger<Program>>();
            //logger.LogInformation(new EventId(201,"c"),"hi");

            Console.ReadKey();
        }
    }
}

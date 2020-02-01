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
            //�����Ϳ�����Startup��ʹ��ConfigureContainer�����ˣ�����ʹ��һ��ǿ����ContainerBuilder
            //�������������Startup�о��޷�ʹ��ConfigureContainer��
            //����Populate֮����ConfigureServices��ע��ķ����ͱ�ע�ᵽ��Autofac����������
        .ConfigureServices(services => services.AddAutofac())
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder
                .UseStartup<Startup>();
        });
    }
}

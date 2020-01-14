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
            //CreateHostBuilder��������Ҫ���׸��ģ�Entity Framework Core��Toolsϣ���õ������������APP��������֮ǰʹ��һЩ����
            var host = CreateHostBuilder(args).Build();

            Person p = new Person("���ڲ�");
            var t = new TShirts();
            var big = new BigTrouser();
            big.Decorate(t);
            t.Decorate(p);
            big.Show();

            host.Run();

        }

        #region Ĭ�ϵķ�ʽ
        public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)//CreateDefaultBuilder������һ������Build�Ķ������ֶ�����Խ�����ʽ
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();//��������ʽʹ��Startup
        })
        .ConfigureServices(services=> {
            services.AddTransient<IStartupFilter, RequestSetOptionsStartupFilter>();
        });

        #endregion

        #region ����˳�� ConfigureHostConfiguration��ConfigureAppConfiguration��ConfigureService��Startup.ConfigureServices��Configure
        //    public static IHostBuilder CreateHostBuilder(string[] args) =>
        //Host.CreateDefaultBuilder(args)//CreateDefaultBuilder������һ������Build�Ķ������ֶ�����Խ�����ʽ
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

        #region ���ַ�ʽ�޷�������վ
        //        public static IHostBuilder CreateHostBuilder(string[] args) =>
        //Host.CreateDefaultBuilder(args); 
        #endregion

        #region ��ʹ��Startup
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

        //                //ÿһ��Use��ͷ�ķ�������������ܵ�������һ���м��
        //                //�м����������һ�����ܾ���Invoke��һ���м��
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

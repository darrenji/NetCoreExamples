using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace TotalNetCore.StartupDemo
{
    /// <summary>
    /// 在Development阶段，可以定义一个StartupDevelopment.cs的文件
    /// </summary>
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //Console.WriteLine("Startup构造函数");
        }

        public IConfiguration Configuration { get; }

        // 在应用程序启动起来调用这里，依赖注入服务
        public void ConfigureServices(IServiceCollection services)
        {
            //Console.WriteLine("Startup.ConfigureServices");
            services.AddRazorPages();
        }

        // 在应用程序启动起来时调用这里，定义请求管道
        //IApplicationBuilder不在ConfitureService中注册，由Host直接生成
        //IWebHostEnvironment, ILoggerFactory是可以在Configure中使用的
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Console.WriteLine("Startup.Configure");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            //每一个Use开头的方法就是在请求管道里增加一个中间件
            //中间件还必须有一个功能就是Invoke下一个中间件
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}

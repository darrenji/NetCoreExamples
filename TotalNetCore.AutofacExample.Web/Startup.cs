using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TotalNetCore.AutofacExample.Web.EventsDynamically;

namespace TotalNetCore.AutofacExample.Web
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; private set; }
        public ILifetimeScope AutofacContainer { get; private set; }

        // 在运行时调用，但在ConfigureContainer方法之前
        // 当没有与environment specefic相关的方法时，会默认调用这里的方法
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            //不要返回IServiceProvider，否则ConfigureContainer方法不会被调用
            //services.AddOptions();
        }

        //如果开发环境是Development,会调用这里的方法
        //当这个方法被调用，ConfigureServices方法就不会被调用
        //public void ConfigureDevelopmentServices(IServiceCollection services)
        //{
        //    //注册开发环境下的服务
        //}

        //在Autofac中的注册写在这里
        //这里会在ConfigureServices之后执行
        //不要在这里Build，Build会在AutofacServiceProviderFactory中搞定
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModule());
            builder.RegisterModule(new DemoModule());
        }

        //当开发环境是Production的时候调用
        //当这里被调用，ConfigureContainer就不会被调用
        //public void ConfigureProductionContainer(ContainerBuilder builder)
        //{
        //    //注册生产环境下的component
        //}

        // 在运行时被调用
        //在ConfigureContainer之后调用
        //可以从IApplicationBuilder.ApplicationServices获取到容器并解析服务
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //在请求管道里获取到Autofac容器的方式
            this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        //当当前环境是Staging的时候调用
        //当这里被调用Configure方法就不会被调用
        //public void ConfigureStaging(IApplicationBuilder app, ILoggerFactory loggerFactory)
        //{

        //}
    }
}

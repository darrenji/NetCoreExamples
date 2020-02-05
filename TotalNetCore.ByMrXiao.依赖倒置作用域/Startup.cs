using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TotalNetCore.ByMrXiao.依赖倒置作用域.Services;

namespace TotalNetCore.ByMrXiao.依赖倒置作用域
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddTransient<IOrderService, DisposableOrderService>();//瞬时服务的IDisposable机制由容器接管。在请求结束后对象被释放，Scope中的服务是transient
            //services.AddScoped<IOrderService>(t => new DisposableOrderService());//Scope中的服务是单例
            //services.AddSingleton<IOrderService>(t=> new DisposableOrderService());//应用程序的单例

            //自己创建并放入容器,不会被释放。容器不会帮管理对象的生命周期,也就是当应用程序停掉或关闭，对象也不会被Dispose掉
            //var service = new DisposableOrderService();
            //services.AddSingleton<IOrderService>(service);

            //只要交给容器，容器就会帮管理对象的生命周期，单例的服务放在根容器里
            //services.AddSingleton<IOrderService, DisposableOrderService>();

            //一个坑 
            services.AddTransient<IOrderService, DisposableOrderService>();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //相当于在根容器获取瞬时服务
            //在根容器持续创建对象实例，但是只能在应用程序退出时回收
            //会在根容器里一直创建对象
            //如果请求多次，这里一直创建却不被回收，很可怕的事情！
            //不要在请求管道里调用瞬时服务！！！
            var s = app.ApplicationServices.GetService<IOrderService>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
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
    }
}

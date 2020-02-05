using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using TotalNetCore.ByMrXiao.依赖注入例子.Services;

namespace TotalNetCore.ByMrXiao.依赖注入例子
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

            #region 注册不同生命周期
            services.AddSingleton<IMySingletonService, MySingletonService>();
            services.AddScoped<IMyScopedService, MyScopedService>();
            services.AddTransient<IMyTransientService, MyTransientService>();
            #endregion

            #region 各种注册方式
            services.AddSingleton<IOrderService>(new OrderService());
            //services.AddSingleton<IOrderService>(serviceCollection => {
            //    //serviceCollection.GetService //获取容器里的其它服务
            //    return new OrderServiceEx();
            //});
            #endregion

            #region 尝试注册
            //services.TryAddSingleton<IOrderService, OrderServiceEx>();//对于一个接口不会重复注册不同实现
            //services.TryAddEnumerable(ServiceDescriptor.Singleton<IOrderService, OrderServiceEx>());//可以为一个接口注册不同的实现，一方面避免重复的注册，另一方面可以把一个接口的不同实现都注入进来
            #endregion

            #region 移除或替换
            //services.RemoveAll<IOrderService>();
            //services.Replace(ServiceDescriptor.Singleton<IOrderService, OrderServiceEx>());
            #endregion

            #region 注册泛型
            services.AddSingleton(typeof(IGenericService<>), typeof(GenericService<>));
            #endregion

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TotalNetCore.ByMrXiao.Autofac增强容器能力.Services;

namespace TotalNetCore.ByMrXiao.Autofac增强容器能力
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
            services.AddControllersWithViews();

            //使用默认的方式注入容器
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            //Autofac接替默认的容器实现注入

            #region 最常用的注册方式
            //builder.RegisterType<MyService>().As<IMyService>();
            #endregion

            #region 命名注册
            //当一个接口需要注入多次，并且以不同的命名来区分
            //builder.RegisterType<MyServiceV2>().Named<IMyService>("service2");
            #endregion

            #region 属性注册，没有注入MyNameService
            //builder.RegisterType<MyServiceV2>().As<IMyService>().PropertiesAutowired();
            #endregion

            #region 属性注册，注入MyNameService
            //builder.RegisterType<MyNameService>();//这个实例作为MyServiceV2的属性实例
            //builder.RegisterType<MyServiceV2>().As<IMyService>().PropertiesAutowired();
            #endregion

            #region AOP注册
            //builder.RegisterType<MyInterceptor>();//注册拦截器
            //builder.RegisterType<MyServiceV2>()
            //       .As<IMyService>()
            //       .PropertiesAutowired()//允许其他服务注入到这个类的属性上
            //       .InterceptedBy(typeof(MyInterceptor))//IMyServvice的拦截器，类拦截器需要把基类设计成虚方法允许继承类重载
            //       .EnableInterfaceInterceptors();//开启
            #endregion

            #region 子容器
            //把服务注册到一个子容器里
            
            builder.RegisterType<MyNameService>().InstancePerMatchingLifetimeScope("myscope");//意味着在其它子容器里是获取不到MyNameService
            #endregion
        }

        /// <summary>
        /// Autofac的根容器
        /// </summary>
        public ILifetimeScope AutofacContainer { get; private set; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();


            #region 获取命名的服务
            //var service2 = this.AutofacContainer.ResolveNamed<IMyService>("service2");
            //service2.ShowCode(); 
            #endregion

            #region 获取没有命名的服务
            //var unnamedService = this.AutofacContainer.Resolve<IMyService>();
            //unnamedService.ShowCode();
            #endregion

            #region 属性注册
            //var propertyService = this.AutofacContainer.Resolve<IMyService>();
            //propertyService.ShowCode();
            #endregion

            #region 拦截器
            //var inceptorSerivce = this.AutofacContainer.Resolve<IMyService>();
            //inceptorSerivce.ShowCode();
            #endregion

            #region 子容器
            using(var myscope = AutofacContainer.BeginLifetimeScope("myscope"))
            {
                //在mysope这个子容器里创建任何其它子容器，得到的是单例
                var service0 = myscope.Resolve<MyNameService>();
                using(var scope = myscope.BeginLifetimeScope())
                {
                    var service1 = scope.Resolve<MyNameService>();
                    var service2 = scope.Resolve<MyNameService>();
                    Console.WriteLine($"service1=service0:{service1==service0}");
                    Console.WriteLine($"service1=serice2:{service1==service2}");
                }
            }
            #endregion

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

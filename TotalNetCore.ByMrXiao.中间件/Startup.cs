using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TotalNetCore.ByMrXiao.中间件.Middlewares;

namespace TotalNetCore.ByMrXiao.中间件
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
            services.AddRazorPages();
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
                app.UseExceptionHandler("/Error");
            }

            //注册中间件
            //中间件就是一个委托
            //这个委托有2个入参，一个是HttpContext, 一个是委托，代表着下一个中间件
            //app.Use(async (context, next) =>
            //{
            //    //await context.Response.WriteAsync("hello");
            //    await next();
            //});

            //Context.Response.HasStarted来判断是否已经输出内容

            //app.Map("/abc", abcBuilder => {
            //    abcBuilder.Use(async (context, next) => {
            //        await next();
            //        await context.Response.WriteAsync("hello");
            //    });
            //});

            //app.MapWhen(context => {
            //    return context.Request.Query.Keys.Contains("abc");
            //}, builder => {
            //    //Run不再执行后续的中间件
            //    //Use对后续的中间件有更多的选择
            //    builder.Run(async context => {
            //        await context.Response.WriteAsync("new abc");
            //    });
            //});


            app.UseMyMiddleware();


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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace TotalNetCore.ByMrXiao.静态文件中间件
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        const int BufferSize = 64 * 1024;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            //services.AddDirectoryBrowser();// 显示文件夹里的内容列表
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

            //app.UseDirectoryBrowser();//显示文件夹里的内容列表
            //app.UseDefaultFiles();//放在UseStaticFiles前面



            app.UseStaticFiles();
            //以上在wwwroot中寻找
            //以下在其它目录中寻找
            app.UseStaticFiles(new StaticFileOptions { 
                RequestPath="/pages",
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "pages"))
            });

            app.MapWhen(context => { 
                //当请求不是api开始的时候
                return !context.Request.Path.Value.StartsWith("/api");
            
            }, appBuilder => {

                //var options = new RewriteOptions();
                //options.AddRewrite(".*","/index.html",true);
                //appBuilder.UseRewriter(options);
                //appBuilder.UseStaticFiles();

                #region 断路器的方式
                appBuilder.Run(async c => {
                    var file = env.WebRootFileProvider.GetFileInfo("index.html");
                    c.Response.ContentType = "text/html";

                    using (var fileStream = new FileStream(file.PhysicalPath, FileMode.Open, FileAccess.Read))
                    {
                        await StreamCopyOperation.CopyToAsync(fileStream, c.Response.Body, null, BufferSize,c.RequestAborted);
                    }
                });
                #endregion

            });
            

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

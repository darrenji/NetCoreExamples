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

        // ������ʱ���ã�����ConfigureContainer����֮ǰ
        // ��û����environment specefic��صķ���ʱ����Ĭ�ϵ�������ķ���
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            //��Ҫ����IServiceProvider������ConfigureContainer�������ᱻ����
            //services.AddOptions();
        }

        //�������������Development,���������ķ���
        //��������������ã�ConfigureServices�����Ͳ��ᱻ����
        //public void ConfigureDevelopmentServices(IServiceCollection services)
        //{
        //    //ע�Ὺ�������µķ���
        //}

        //��Autofac�е�ע��д������
        //�������ConfigureServices֮��ִ��
        //��Ҫ������Build��Build����AutofacServiceProviderFactory�и㶨
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModule());
            builder.RegisterModule(new DemoModule());
        }

        //������������Production��ʱ�����
        //�����ﱻ���ã�ConfigureContainer�Ͳ��ᱻ����
        //public void ConfigureProductionContainer(ContainerBuilder builder)
        //{
        //    //ע�����������µ�component
        //}

        // ������ʱ������
        //��ConfigureContainer֮�����
        //���Դ�IApplicationBuilder.ApplicationServices��ȡ����������������
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

            //������ܵ����ȡ��Autofac�����ķ�ʽ
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

        //����ǰ������Staging��ʱ�����
        //�����ﱻ����Configure�����Ͳ��ᱻ����
        //public void ConfigureStaging(IApplicationBuilder app, ILoggerFactory loggerFactory)
        //{

        //}
    }
}

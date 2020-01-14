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
    /// ��Development�׶Σ����Զ���һ��StartupDevelopment.cs���ļ�
    /// </summary>
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //Console.WriteLine("Startup���캯��");
        }

        public IConfiguration Configuration { get; }

        // ��Ӧ�ó����������������������ע�����
        public void ConfigureServices(IServiceCollection services)
        {
            //Console.WriteLine("Startup.ConfigureServices");
            services.AddRazorPages();
        }

        // ��Ӧ�ó�����������ʱ���������������ܵ�
        //IApplicationBuilder����ConfitureService��ע�ᣬ��Hostֱ������
        //IWebHostEnvironment, ILoggerFactory�ǿ�����Configure��ʹ�õ�
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
            //ÿһ��Use��ͷ�ķ�������������ܵ�������һ���м��
            //�м����������һ�����ܾ���Invoke��һ���м��
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

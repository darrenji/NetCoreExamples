using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TotalNetCore.Networking.API.Services;

namespace TotalNetCore.Networking.API
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
            //services.AddHttpClient();//IHttpClientFactory��HttpClientFactory

            services.AddSingleton<IDataStoreClient, FitnessDataStoreClient>();

            services.AddHttpClient("WRITER", c => {//��ȷʵ����
                c.BaseAddress = new Uri("http://fitness.write.data.com:56789");
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            services.AddHttpClient("READER", c => {
                c.BaseAddress = new Uri("http://fitness.read.data.com:56789");
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDD.Marketplace.Adapter;
using DDD.Marketplace.Api;
using DDD.Marketplace.Domain;
using DDD.Marketplace.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Raven.Client.Documents;

namespace DDD.Marketplace
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
            //var store = new DocumentStore { 
            //    Urls = new[] { "http://localhost:8080"},
            //    Database = "",
            //    Conventions = {FindIdentityProperty = m => m.Name == "" }
            //};
            //store.Initialize();

            services.AddSingleton<ICurrencyLookup, FixedCurrencyLookup>();
            //services.AddScoped(c => store.OpenAsyncSession());
            //services.AddScoped<IUnitOfWork, RavenDbUnitOfWork>();
            services.AddScoped<IUnitOfWork, EfCoreUnitOfWork>();
            services.AddScoped<IClassifiedAdRepository, ClassifeidAdRepository>();
            services.AddScoped<ClassifiedAdsApplicationService>();


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

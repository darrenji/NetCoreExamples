using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Hellang.Middleware.ProblemDetails;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TotalNetCore.DDDAPISample.API.Configuration;
using TotalNetCore.DDDAPISample.API.SeedWork;
using TotalNetCore.DDDAPISample.Application.Configuration.Validation;
using TotalNetCore.DDDAPISample.Application.Customers.RegisterCustomer;
using TotalNetCore.DDDAPISample.Domain.Shared;
using TotalNetCore.DDDAPISample.Infrastructure;
using TotalNetCore.DDDAPISample.Infrastructure.Database;
using TotalNetCore.DDDAPISample.Infrastructure.Shared;

namespace TotalNetCore.DDDAPISample.API
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            this._configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json")
                //.AddJsonFile($"hosting.{env.EnvironmentName}.json")
                //.AddUserSecrets<Startup>()
                .Build();
        }

        private readonly IConfiguration _configuration;
        private const string OrdersConnectionString = "OrdersConnectionString";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddMemoryCache();

            services.AddSwaggerDocumentation();

            //services.AddMediatR(typeof(RegisterCustomerCommandHandler).GetTypeInfo().Assembly);

            services
                .AddDbContext<OrdersContext>(options =>
                {
                    options
                        .ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>()

                        .UseSqlServer(this._configuration[OrdersConnectionString]);
                });

            services.AddProblemDetails(x =>
            {
                x.Map<InvalidCommandException>(ex => new InvalidCommandProblemDetails(ex));
                x.Map<BusinessRuleValidationException>(ex => new BusinessRuleValidationExceptionProblemDetails(ex));
            });

            var children = this._configuration.GetSection("Caching").GetChildren();
            var cachingConfiguration = children.ToDictionary(child => child.Key, child => TimeSpan.Parse(child.Value));

             ApplicationStartup.Initialize(
                services,
                this._configuration[OrdersConnectionString],
                cachingConfiguration);
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

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TotalNetCore.UnitTest.EmployeeApp;
using TotalNetCore.UnitTest.EmployeeApp.Models;

namespace TotalNetCore.UnitTest.EmployeeIntegrationTest
{
    /// <summary>
    /// 实现一个内置的父类，体现了Open-Close原则，对修改封闭，对扩展开放
    /// </summary>
    public class TestingWebAppFactory<T> : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services => {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<EmployeeContext>));
                if (descriptor != null) services.Remove(descriptor);

                var serviceProvider = new ServiceCollection()
                     .AddEntityFrameworkInMemoryDatabase()
                     .BuildServiceProvider();

                services.AddDbContext<EmployeeContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryEmployeeTest");
                    options.UseInternalServiceProvider(serviceProvider);
                });

                var sp = services.BuildServiceProvider();

                using(var scope = sp.CreateScope())
                {
                    using(var appContext = scope.ServiceProvider.GetRequiredService<EmployeeContext>())
                    {
                        try
                        {
                            appContext.Database.EnsureCreated();
                        }
                        catch (Exception ex)
                        {

                            throw;
                        }
                    }
                }
            });
        }
    }
}

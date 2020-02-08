using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotalNetCore.ByMrXiao.服务组件配置.Services
{
    public static class OrderServiceExtensions
    {
        public static IServiceCollection AddOrderService(this IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            services.Configure<OrderServiceOptions>(configuration);

            //定义实例化服务之后的自动动作
            services.ConfigureAll<OrderServiceOptions>(options => { options.MaxOrderCount += 100; });


            //services.AddScoped<IOrderService, OrderService>();//scope
            services.AddSingleton<IOrderService, OrderService>();//单例
            return services;
        }
    }
}

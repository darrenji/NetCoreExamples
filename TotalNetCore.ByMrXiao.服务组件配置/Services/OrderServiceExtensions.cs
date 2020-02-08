using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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
            #region 通常的写法
            //services.Configure<OrderServiceOptions>(configuration); 
            #endregion

            #region 验证方式1
            //services.AddOptions<OrderServiceOptions>().Configure(options =>
            //{
            //    configuration.Bind(options);
            //}).Validate(options => {
            //    return options.MaxOrderCount < 100;
            //}, "MaxOrderCount不能小于100");
            #endregion

            #region 验证方式二
            //services.AddOptions<OrderServiceOptions>().Configure(options =>
            //{
            //    configuration.Bind(options);
            //}).ValidateDataAnnotations();
            #endregion

            #region 验证方式三
            services.AddOptions<OrderServiceOptions>().Configure(options =>
            {
                configuration.Bind(options);
            }).Services.AddSingleton<IValidateOptions<OrderServiceOptions>, OrderServieValidateOptions>();
            #endregion

            //定义实例化服务之后的自动动作
            //services.PostConfigure<OrderServiceOptions>(options => { options.MaxOrderCount += 100; });


            //services.AddScoped<IOrderService, OrderService>();//scope
            services.AddSingleton<IOrderService, OrderService>();//单例
            return services;
        }
    }
}

using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.ByMrXiao.配置
{
    public static class MyConfigurationBuilderExtensions
    {
        //通过扩展方法暴露自定义的服务
        public static IConfigurationBuilder AddMyConfiguration(this IConfigurationBuilder builder)
        {
            builder.Add(new MyConfigurationSource());
            return builder;
        }
    }
}

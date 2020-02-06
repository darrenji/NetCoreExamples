using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotalNetCore.ByMrXiao.Autofac增强容器能力.Services
{
    public class MyInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine($"Interceptor before, Method:{invocation.Method.Name}");
            invocation.Proceed();//具体方法的执行,如果禁用就不会执行服务的方法了
            Console.WriteLine($"Interceptor after, Method:{invocation.Method.Name}");
        }
    }
}

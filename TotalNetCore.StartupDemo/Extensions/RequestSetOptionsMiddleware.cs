using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace TotalNetCore.StartupDemo.Extensions
{
    /// <summary>
    /// 所有的中间件都处理HttpContext,处理完后交给下一个中间件
    /// </summary>
    public class RequestSetOptionsMiddleware
    {
        private readonly RequestDelegate _next;

        //在构造函数中注入下一个中间件，其实中间件就是RequestDelegate
        //给到中间件的对象就是HttpContext
        public RequestSetOptionsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var option = httpContext.Request.Query["options"];
            if(!string.IsNullOrEmpty(option))
            {
                httpContext.Items["options"] = WebUtility.HtmlEncode(option);
            }
            await _next(httpContext);
        }
    }
}

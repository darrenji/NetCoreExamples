using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TotalNetCore.ByMrXiao.依赖倒置作用域.Models;
using TotalNetCore.ByMrXiao.依赖倒置作用域.Services;

namespace TotalNetCore.ByMrXiao.依赖倒置作用域.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //scope的写法
        //public string GetService([FromServices]IOrderService orderService, [FromServices]IOrderService orderService2)
        //{

        //    Console.WriteLine("==1==");
        //    //根容器 IServiceProvider
        //    //子容器IServiceScope的边界或者域，子容器也是IServiceProvider
        //    using (IServiceScope scope = HttpContext.RequestServices.CreateScope())//RequetServices得到当前请求IServiceProvider的根容器，CreateScope创建一个子容器IServiceScope
        //    {
        //        //相当于Scope中的单例
        //        var service = scope.ServiceProvider.GetService<IOrderService>();
        //        var service2 = scope.ServiceProvider.GetService<IOrderService>();//每个作用域内不管声明多少次，只产生一个实例，即在域内是单例
        //    }

        //    Console.WriteLine("==2==");

        //    Console.WriteLine("GetService方法已经被调用");
        //    return "Get Service";
        //}

        /*
         Transient:以上两个服务在方法执行结束后释放
         Scoped:一个实例在using结束之后Dispose掉，也就是在scope生命周期结束之后Dispose掉；另一个实例在方法调用之后Dispose掉。
         */

        public string GetService([FromServices]IOrderService orderService, [FromServices]IOrderService orderService2,[FromServices]IHostApplicationLifetime hostApplicationLifetime,[FromQuery]bool stop=false)
        {
            if(stop)
            {
                hostApplicationLifetime.StopApplication();
            }

            Console.WriteLine("GetService方法已经被调用");
            return "Get Service";
        }
    }
}

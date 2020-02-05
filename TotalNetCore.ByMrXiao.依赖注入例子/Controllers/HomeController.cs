using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TotalNetCore.ByMrXiao.依赖注入例子.Models;
using TotalNetCore.ByMrXiao.依赖注入例子.Services;

namespace TotalNetCore.ByMrXiao.依赖注入例子.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOrderService _orderService;

        public HomeController(ILogger<HomeController> logger, IOrderService orderService, IGenericService<IOrderService> genericService)
        {
            _logger = logger;
            _orderService = orderService;
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

        public String GetService([FromServices]IMySingletonService singleton1,
            [FromServices]IMySingletonService singleton2,
            [FromServices]IMyScopedService scoped1,
            [FromServices]IMyScopedService scoped2,
            [FromServices]IMyTransientService transient1,
            [FromServices]IMyTransientService transient2)
        {
            Console.WriteLine($"singleton1:{singleton1.GetHashCode()}");
            Console.WriteLine($"singleton2:{singleton2.GetHashCode()}");

            Console.WriteLine($"scoped1:{scoped1.GetHashCode()}");
            Console.WriteLine($"scoped2:{scoped2.GetHashCode()}");

            Console.WriteLine($"transient1:{transient1.GetHashCode()}");
            Console.WriteLine($"transient2:{transient2.GetHashCode()}");

            Console.WriteLine("请求结束");

            return "Get servies from IoC container";
        }

        public string GetServiceList([FromServices]IEnumerable<IOrderService> services)
        {
            foreach(var item in services)
            {
                Console.WriteLine($"获取到的服务实例为：{item.ToString()}:{item.GetHashCode()}");
            }
            return "get services from one interface";
        }
    }
}

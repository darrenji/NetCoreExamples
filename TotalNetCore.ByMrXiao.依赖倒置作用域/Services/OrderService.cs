using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotalNetCore.ByMrXiao.依赖倒置作用域.Services
{
    public interface IOrderService
    {

    }
    public class DisposableOrderService : IOrderService, IDisposable
    {
        //public void Dispose()
        //{
        //    Console.WriteLine($"DisposableOrderService Disposed:{this.GetHashCode()}");
        //}
        public void Dispose()
        {
            Console.WriteLine($"DisposableOrderService Disposed:{this.GetHashCode()}");
        }
    }
}

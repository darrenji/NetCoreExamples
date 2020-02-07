using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotalNetCore.ByMrXiao.服务组件配置.Services
{
    public interface IOrderService
    {
        int ShowMaxOrderCount();
    }

    //最平常的写法
    //public class OrderService : IOrderService
    //{
    //    OrderServiceOptions _options;

    //    public OrderService(OrderServiceOptions options)
    //    {
    //        this._options = options;
    //    }

    //    public int ShowMaxOrderCount()
    //    {
    //        return _options.MaxOrderCount;
    //    }
    //}


    public class OrderService : IOrderService
    {
        IOptions<OrderServiceOptions> _options;

        public OrderService(IOptions<OrderServiceOptions> options)
        {
            this._options = options;
        }

        public int ShowMaxOrderCount()
        {
            return _options.Value.MaxOrderCount;
        }
    }

    public class OrderServiceOptions
    {
        public int MaxOrderCount { get; set; } = 100;
    }
}

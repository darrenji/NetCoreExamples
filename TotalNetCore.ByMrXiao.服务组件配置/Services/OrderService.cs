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

    #region 组件内配置
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


        //使用IOptions<>的写法
    //public class OrderService : IOrderService
    //{
    //    IOptions<OrderServiceOptions> _options;

    //    public OrderService(IOptions<OrderServiceOptions> options)
    //    {
    //        this._options = options;
    //    }

    //    public int ShowMaxOrderCount()
    //    {
    //        return _options.Value.MaxOrderCount;
    //    }
    //}
    #endregion

    #region 服务感知配置变化

       //Scope服务感知配置变化
    public class OrderService : IOrderService
    {
        #region scope
        //IOptionsSnapshot<OrderServiceOptions> _options;

        //public OrderService(IOptionsSnapshot<OrderServiceOptions> options)
        //{
        //    this._options = options;
        //} 
        #endregion

        #region singleton
        IOptionsMonitor<OrderServiceOptions> _options;

        public OrderService(IOptionsMonitor<OrderServiceOptions> options)
        {
            this._options = options;

            //侦听变化
            _options.OnChange(o => {

                Console.WriteLine($"配置发生了变化：{o.MaxOrderCount}");
            });
        } 
        #endregion

        public int ShowMaxOrderCount()
        {
            //return _options.Value.MaxOrderCount; //scope
            return _options.CurrentValue.MaxOrderCount;//singleton
        }
    }
    #endregion

    public class OrderServiceOptions
    {
        public int MaxOrderCount { get; set; } = 100;
    }
}

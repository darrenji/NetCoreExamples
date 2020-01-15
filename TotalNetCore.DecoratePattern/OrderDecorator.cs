using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.DecoratePattern
{
    /// <summary>
    /// 把抽象基类给装饰器类，装饰器类和抽象基类以及子类具备相同的行为
    /// </summary>
    public class OrderDecorator : OrderBase
    {
        protected OrderBase order;

        public OrderDecorator(OrderBase order)
        {
            this.order = order;
        }

        public override double CalculateTotalOrderPrice()
        {
            Console.WriteLine($"Calculating the total price in a decorator class");
            return order.CalculateTotalOrderPrice();
        }
    }
}

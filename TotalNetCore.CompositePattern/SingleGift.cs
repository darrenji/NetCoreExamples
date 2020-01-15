using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.CompositePattern
{
    public class SingleGift : GiftBase
    {
        public SingleGift(string name, int price) : base(name, price)
        {

        }
        public override int CalculateTotalPrice()
        {
            Console.WriteLine($"产品{name}的价格是：{price}");
            return price;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.CompositePattern
{
    //一个Composite下包含实现同一个抽象基类的集合
    public class CompositeGift : GiftBase, IGiftOperations
    {
        private List<GiftBase> _gifts;

        public CompositeGift(string name, int price) : base(name, price)
        {
            _gifts = new List<GiftBase>();
        }
        public void Add(GiftBase gift)
        {
            _gifts.Add(gift);
        }

        public void Remove(GiftBase gift)
        {
            _gifts.Remove(gift);
        }

        public override int CalculateTotalPrice()
        {
            int total = 0;

            Console.WriteLine($"产品{name}下拥有以下产品和价格：");

            foreach(var gift in _gifts)
            {
                total += gift.CalculateTotalPrice();
            }

            return total;
        }
    }
}

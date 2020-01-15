using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.DecoratePattern
{
    public class PremiumPreorder : OrderDecorator
    {

        public PremiumPreorder(OrderBase order) : base(order)
        {

        }

        public override double CalculateTotalOrderPrice()
        {
            Console.WriteLine($"Calculating the total price in the {nameof(PremiumPreorder)} class.");
            var preOrderPrice = base.CalculateTotalOrderPrice();

            Console.WriteLine("Adding additional discount to a preorder price");
            return preOrderPrice * 0.9;
        }
    }
}

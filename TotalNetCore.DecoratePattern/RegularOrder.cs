using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TotalNetCore.DecoratePattern
{
    public class RegularOrder : OrderBase
    {
        public override double CalculateTotalOrderPrice()
        {
            Console.WriteLine("Calculating the total price of a regular order");
            return products.Sum(x => x.Price);
        }
    }
}

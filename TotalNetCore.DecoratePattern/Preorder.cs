﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TotalNetCore.DecoratePattern
{
    public class Preorder : OrderBase
    {
        public override double CalculateTotalOrderPrice()
        {
            Console.WriteLine("Calculating the total price of a preorder.");
            return products.Sum(x => x.Price) * 0.9;
        }
    }
}

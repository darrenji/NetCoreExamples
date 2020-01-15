using System;

namespace TotalNetCore.DecoratePattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var preOrder = new Preorder();
            Console.WriteLine(preOrder.CalculateTotalOrderPrice());
            Console.WriteLine();

            var premiumPreorder = new PremiumPreorder(preOrder);
            Console.WriteLine(premiumPreorder.CalculateTotalOrderPrice());
        }
    }
}

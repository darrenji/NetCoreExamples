using System;

namespace TotalNetCore.CompositePattern
{
    class Program
    {
        static void Main(string[] args)
        {
            //var phone = new SingleGift("Phone", 100);
            //phone.CalculateTotalPrice();
            //Console.WriteLine();

            var rootBox = new CompositeGift("RootBox", 10);
            var truckToy = new SingleGift("TruncType", 20);
            var plainToy = new SingleGift("PlainToy", 30);
            rootBox.Add(truckToy);
            rootBox.Add(plainToy);

            Console.WriteLine($"总价:{rootBox.CalculateTotalPrice()}");
        }
    }
}

using System;

namespace TotalNetCore.CommandPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            //对一个对象执行一系列动作
            var modifyPrice = new ModifyPrice();
            var product = new Product("Phone", 50);

            Execute(product, modifyPrice, new ProductCommand(product, PriceAction.Increase, 100));
            Execute(product, modifyPrice, new ProductCommand(product, PriceAction.Increase, 200));
            Execute(product, modifyPrice, new ProductCommand(product, PriceAction.Decrease, 200));

            Console.WriteLine(product);

        }

        private static void Execute(Product product, ModifyPrice modifyPrice, ICommand productCommand)
        {
            modifyPrice.SetCommand(productCommand);
            modifyPrice.Invoke();
        }
    }
}

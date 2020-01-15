using System;
using System.Collections.Generic;

namespace TotalNetCore.BuilderPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 不用链式
            //var products = new List<Product>
            //{
            //    new Product{ Name="product1", Price=11.20},
            //    new Product{ Name="product2", Price=30.5}
            //};

            //var builder = new ProductStockReportBuilder(products);
            //var director = new ProductStockReportDirector(builder);
            //director.BuildStockReport();
            //var report = builder.GetReport();
            //Console.WriteLine(report.ToString());
            #endregion

            #region 使用链式
            var products = new List<Product>
            {
                new Product{ Name="product1", Price=11.20},
                new Product{ Name="product2", Price=30.5}
            };

            var builder = new ProductStockReportChainedBuilder(products);
            var director = new ProductStockReportChainedDirector(builder);
            director.BuildStockReport();

            var report = builder.GetReport();
            Console.WriteLine(report.ToString());

            //在接口中定义好所有的构造细节
            #endregion
        }
    }
}

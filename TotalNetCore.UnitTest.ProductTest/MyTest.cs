using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.UnitTest.EmployeeApp.Controllers;
using TotalNetCore.UnitTest.EmployeeApp.Models;
using Xunit;

namespace TotalNetCore.UnitTest.ProductTest
{
    public class MyTest
    {

        private ProductDbContext GetContextWithData()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                              .UseInMemoryDatabase(Guid.NewGuid().ToString())
                              .Options;
            var context = new ProductDbContext(options);

            var beerCategory = new Category { Id = 1, Name = "Beers" };
            var wineCategory = new Category { Id = 2, Name = "Wines" };
            context.Categories.Add(beerCategory);
            context.Categories.Add(wineCategory);

            context.Products.Add(new Product { Id = 1, Name = "La Trappe Isid'or", Category = beerCategory });
            context.Products.Add(new Product { Id = 2, Name = "St. Bernardus Abt 12", Category = beerCategory });
            context.Products.Add(new Product { Id = 3, Name = "Zundert", Category = beerCategory });
            context.Products.Add(new Product { Id = 4, Name = "La Trappe Blond", Category = beerCategory });
            context.Products.Add(new Product { Id = 5, Name = "La Trappe Bock", Category = beerCategory });
            context.Products.Add(new Product { Id = 6, Name = "St. Bernardus Tripel", Category = beerCategory });
            context.Products.Add(new Product { Id = 7, Name = "Grottenbier Bruin", Category = beerCategory });
            context.Products.Add(new Product { Id = 8, Name = "St. Bernardus Pater 6", Category = beerCategory });
            context.Products.Add(new Product { Id = 9, Name = "La Trappe Quadrupel", Category = beerCategory });
            context.Products.Add(new Product { Id = 10, Name = "Westvleteren 12", Category = beerCategory });
            context.Products.Add(new Product { Id = 11, Name = "Leffe Bruin", Category = beerCategory });
            context.Products.Add(new Product { Id = 12, Name = "Leffe Royale", Category = beerCategory });
            context.SaveChanges();

            return context;
        }

        [Fact(DisplayName = "Index should return default view")]
        public void Index_should_return_default_view()
        {
            using (var context = GetContextWithData())
            using (var controller = new ProductsController(context))
            {
                var result = controller.Index() as ViewResult;

                Assert.NotNull(result);
                Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Index");
            }
        }
    }
}

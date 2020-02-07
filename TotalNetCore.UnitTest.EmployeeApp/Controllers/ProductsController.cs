using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TotalNetCore.UnitTest.EmployeeApp.Models;

namespace TotalNetCore.UnitTest.EmployeeApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductDbContext _context;

        public ProductsController(ProductDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var model = new FrontPageModel();
            model.Top5 = _context.Products.Take(5);
            model.Items = _context.Products.Take(10);

            return View(model);
        }

        public IActionResult Category(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            var products = _context.Products.Where(p => p.Category.Id == id);
            return View(products);
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposing)
            {
                _context.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
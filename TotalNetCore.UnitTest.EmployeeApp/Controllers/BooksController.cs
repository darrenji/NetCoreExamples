using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TotalNetCore.UnitTest.EmployeeApp.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TotalNetCore.UnitTest.EmployeeApp.Controllers
{
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private readonly BooksContext _booksContext;

        public BooksController(BooksContext booksContext)
        {
            _booksContext = booksContext;
        }

        [HttpGet]
        public IEnumerable<Book> Get() => _booksContext.Books;

        [HttpGet("{id}")]
        public Book Get(int id) => _booksContext.Books.Find(id);
    }
}

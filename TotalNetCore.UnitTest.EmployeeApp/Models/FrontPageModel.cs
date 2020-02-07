using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotalNetCore.UnitTest.EmployeeApp.Models
{
    public class FrontPageModel
    {
        public IEnumerable<Product> Top5;
        public IEnumerable<Product> Items;
    }
}

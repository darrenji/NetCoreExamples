using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.FluentBuilderWithRecursive
{
    public class Employee
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public double Salary { get; set; }


        public override string ToString()
        {
            return $"Name：{Name}, Position:{Position}, Salary:{Salary}";
        }
    }


}

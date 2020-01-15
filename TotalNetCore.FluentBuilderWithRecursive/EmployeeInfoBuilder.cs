using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.FluentBuilderWithRecursive
{
    public class EmployeeInfoBuilder
    {
        protected Employee employee = new Employee();

        public EmployeeInfoBuilder SetName(string name)
        {
            employee.Name = name;
            return this;
        }
    }
}

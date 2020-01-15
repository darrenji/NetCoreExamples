using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.FluentBuilderWithRecursive
{
    public abstract class EmployeeBuilder
    {
        protected Employee employee;

        public EmployeeBuilder()
        {
            employee = new Employee();
        }

        public Employee Build() => employee;
    }
}

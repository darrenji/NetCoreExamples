using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.FluentBuilderWithRecursive
{
    public class EmployeePositionBuilder : EmployeeInfoBuilder
    {
        public EmployeePositionBuilder AtPosition(string position)
        {
            employee.Position = position;
            return this;
        }
    }
}

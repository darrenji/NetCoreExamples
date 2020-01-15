using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.FluentBuilderWithRecursive
{
    public class NewEmployeeSalaryBuilder<T> : NewEmployeePositionBuilder<NewEmployeeSalaryBuilder<T>> where T : NewEmployeeSalaryBuilder<T>
    {
        public T WithSalary(double salary)
        {
            employee.Salary = salary;
            return (T)this;
        }
    }
}

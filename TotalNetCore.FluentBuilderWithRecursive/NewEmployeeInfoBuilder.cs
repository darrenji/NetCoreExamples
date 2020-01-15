using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.FluentBuilderWithRecursive
{
    //这里的T是为子类腾出位置，这样父类就可以知道子类了
    public class NewEmployeeInfoBuilder<T> : EmployeeBuilder where T :NewEmployeeInfoBuilder<T>
    {
        public T SetName(string name)
        {
            employee.Name = name;
            return (T)this;
        }
    }
}

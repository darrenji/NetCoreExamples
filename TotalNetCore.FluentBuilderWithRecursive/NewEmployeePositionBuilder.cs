using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.FluentBuilderWithRecursive
{
    public class NewEmployeePositionBuilder<T> : NewEmployeeInfoBuilder<NewEmployeePositionBuilder<T>> where T : NewEmployeePositionBuilder<T>
    {
        public T AtPosition(string position)
        {
            employee.Position = position;
            return (T)this;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.FluentBuilderWithRecursive
{
    //其实每个父类都通过泛型知道它的子类
    public class NewEmployeeBuilderDirector : NewEmployeeSalaryBuilder<NewEmployeeBuilderDirector>
    {
        public static NewEmployeeBuilderDirector NewEmployee => new NewEmployeeBuilderDirector();
    }
}

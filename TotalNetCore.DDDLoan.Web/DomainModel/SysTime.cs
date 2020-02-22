using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotalNetCore.DDDLoan.Web.DomainModel
{
    public class SysTime
    {
        public static Func<DateTime> CurrentTimeProvider { get; set; } = () => DateTime.Now;

        //想得到DateTime，使用委托来生成DateTime实例，相当于工厂方法
        public static DateTime Now() => CurrentTimeProvider();
    }
}

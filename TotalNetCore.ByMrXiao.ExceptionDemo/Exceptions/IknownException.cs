using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotalNetCore.ByMrXiao.ExceptionDemo.Exceptions
{
    public interface IknownException
    {
        string Message { get; }
        int ErrorCode { get; }
        object[] ErrorData { get; }

    }
}

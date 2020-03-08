using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotalNetCore.ByMrXiao.ExceptionDemo.Exceptions
{
    public class MyServerException : Exception, IknownException
    {
        public int ErrorCode { get; private set; }

        public object[] ErrorData { get; private set; }

        public MyServerException(string message, int errorCode, params object[] errorData) : base(message)
        {
            this.ErrorCode = errorCode;
            this.ErrorData = errorData;
        }

    }
}

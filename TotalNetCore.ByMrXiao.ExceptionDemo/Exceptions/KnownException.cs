using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotalNetCore.ByMrXiao.ExceptionDemo.Exceptions
{
    public class KnownException : IknownException
    {
        public string Message { get; private set; }

        public int ErrorCode { get; private set; }

        public object[] ErrorData { get; private set; }

        public readonly static IknownException Unknown = new KnownException { Message = "未知错误", ErrorCode = 9999 };

        public static IknownException FromKnownException(IknownException exceptoin)
        {
            return new KnownException { Message = exceptoin.Message, ErrorCode = exceptoin.ErrorCode, ErrorData = exceptoin.ErrorData };
        }
    }
}

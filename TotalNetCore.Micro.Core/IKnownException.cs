using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.Micro.Core
{
   public  interface IKnownException
    {
        string Message { get; }

        int ErrorCode { get; }

        object[] ErrorData { get; }
    }
}

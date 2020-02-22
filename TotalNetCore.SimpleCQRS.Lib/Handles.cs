using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.SimpleCQRS.Lib
{
    public interface Handles<T>
    {
        void Handle(T message);
    }
}

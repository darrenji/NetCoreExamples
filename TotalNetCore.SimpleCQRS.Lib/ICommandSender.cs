using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.SimpleCQRS.Lib
{
    public interface ICommandSender
    {
        void Send<T>(T command) where T : Command;
    }
}

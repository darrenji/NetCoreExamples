using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.CoffeeDDD.Common
{
    public interface ICommandSender
    {
        void Send<T>(T command) where T : Command;
    }
}

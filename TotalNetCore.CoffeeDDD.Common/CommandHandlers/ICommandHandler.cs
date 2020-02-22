using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.CoffeeDDD.Common
{
    public interface ICommandHandler<TCommand> where TCommand : Message
    {
        void Handle(TCommand message);
    }
}

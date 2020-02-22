using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.CoffeeDDD.Common
{
    public class Command : Message
    {
        public Guid Id { get; }

        public Command()
        {
            Id = Guid.NewGuid();
        }
    }
}

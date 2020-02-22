using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.CoffeeDDD.Common
{
    public class Event : Message
    {
        public Guid Id;
        public int Version;

        public Event()
        {
            Id = Guid.NewGuid();
        }
    }
}

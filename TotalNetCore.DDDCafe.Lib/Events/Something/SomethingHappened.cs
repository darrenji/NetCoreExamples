using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.DDDCafe.Lib.CQRS;

namespace TotalNetCore.DDDCafe.Lib.Events.Something
{
    public class SomethingHappened : Event
    {
        public Guid Id;
        public string What;
    }
}

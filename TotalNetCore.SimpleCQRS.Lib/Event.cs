using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.SimpleCQRS.Lib
{
   public  class Event : Message
    {
        public int Version { get; set; }
    }
}

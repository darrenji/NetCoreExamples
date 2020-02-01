using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.DDDAPISample.Infrastructure.Processing.InternalCommands
{
    /// <summary>
    /// 记录Command
    /// 属于Domain Entity, 不属于Aggregate
    /// </summary>
    public class InternalCommand
    {
        public Guid Id { get; set; }

        public string Type { get; set; }

        public string Data { get; set; }

        public DateTime? ProcessedDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.DDDAPISample.Infrastructure.Processing.Outbox
{
    public class OutboxMessageDto
    {
        public Guid Id { get; set; }

        public string Type { get; set; }

        public string Data { get; set; }
    }
}

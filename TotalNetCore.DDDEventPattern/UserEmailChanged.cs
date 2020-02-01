using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.DDDEventPattern
{
    public class UserEmailChanged : IEvent
    {
        public Guid Id { get; }
        public UserEmailChanged(Guid id)
        {
            Id = id;
        }
    }
}

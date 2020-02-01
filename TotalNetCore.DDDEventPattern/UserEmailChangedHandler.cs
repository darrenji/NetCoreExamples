using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TotalNetCore.DDDEventPattern
{
    public class UserEmailChangedHandler : IEventHandler<UserEmailChanged>
    {
        public Task HandleAsync(UserEmailChanged @event)
        {
            // fetch from database
            // log
            // store data
            return Task.CompletedTask;
        }
    }
}

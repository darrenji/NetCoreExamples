using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotalNetCore.DDDEventPattern
{
    public class UserService : IUserService
    {
        private readonly IEventDispatcher _eventDispatcher;

        public UserService(IEventDispatcher eventDispatcher)
        {
            _eventDispatcher = eventDispatcher;
        }
        public async Task CreateAsync(string email)
        {
            var user = new User(email);

            // store

            await _eventDispatcher.DispatchAsync(user.Events.ToArray());
        }
    }
}

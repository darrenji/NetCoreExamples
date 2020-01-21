using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.Marketplace.Api
{
    public class RetryPolicy
    {
        public Task ExecuteAsync(Action action)
        {
            action.Invoke();
            return Task.CompletedTask;
        }
    }
}

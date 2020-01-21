using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.Marketplace.Api
{
    //这个类借助RetryPolicy的某个方法做了一件事，这件事就是IHandleCommand要做的事情
    public class RetryingCommandHandler<T> : IHandleCommand<T>
    {
        static RetryPolicy _policy = Policy
            .Handle<InvalidOperationException>()
            .Retry();

        private IHandleCommand<T> _next;

        public RetryingCommandHandler(IHandleCommand<T> next)
        {
            _next = next;
        }
           
        public async Task Handle(T command)
        {
            await _policy.ExecuteAsync(() => _next.Handle(command));
        }
    }
}

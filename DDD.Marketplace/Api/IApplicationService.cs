using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.Marketplace.Api
{
    public interface IApplicationService
    {
        Task Handle(object command);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Marketplace.Adapter
{
    public interface IApplicationService
    {
        Task Handle(object command);
    }
}

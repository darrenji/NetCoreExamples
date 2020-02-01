using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TotalNetCore.DDDAPISample.Infrastructure.Processing
{
    /// <summary>
    /// 处理数据库中的Command
    /// </summary>
    public interface ICommandsDispatcher
    {
        Task DispatchCommandAsync(Guid id);
    }
}

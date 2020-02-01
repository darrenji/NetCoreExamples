using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotalNetCore.AutofacExample.Web.EventsDynamically
{
    /// <summary>
    /// 处理所有领域的所有事件接口
    /// </summary>
    public interface IDomainEventExecutor
    {
        void Execute(IEnumerable<IEntity> domainEventEntities);
    }
}

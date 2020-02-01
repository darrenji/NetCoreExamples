using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TotalNetCore.DDDAPISample.Domain.Interfaces
{
    /// <summary>
    /// 工作者单元接口
    /// </summary>
    public interface IUnitOfWork
    {
        Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}

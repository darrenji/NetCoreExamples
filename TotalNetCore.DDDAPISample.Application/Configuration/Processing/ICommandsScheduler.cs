using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TotalNetCore.DDDAPISample.Application.Configuration.Processing
{
    /// <summary>
    /// 命令的调度
    /// </summary>
    public interface ICommandsScheduler
    {
        Task EnqueueAsync(IRequest command);
    }
}

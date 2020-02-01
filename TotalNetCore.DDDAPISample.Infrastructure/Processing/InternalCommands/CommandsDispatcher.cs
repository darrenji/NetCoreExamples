using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TotalNetCore.DDDAPISample.Application.Customers;
using TotalNetCore.DDDAPISample.Infrastructure.Database;

namespace TotalNetCore.DDDAPISample.Infrastructure.Processing.InternalCommands
{
    /// <summary>
    /// 处理从数据库中取出的Command
    /// 还是通过MediatR发布出去
    /// </summary>
    public class CommandsDispatcher : ICommandsDispatcher
    {
        private readonly IMediator _mediator;
        private readonly OrdersContext _ordersContext;

        public CommandsDispatcher(
            IMediator mediator,
            OrdersContext ordersContext)
        {
            this._mediator = mediator;
            this._ordersContext = ordersContext;
        }

        public async Task DispatchCommandAsync(Guid id)
        {
            var command = await this._ordersContext.InternalCommands.SingleOrDefaultAsync(x => x.Id == id);

            Type type = Assembly.GetAssembly(typeof(MarkCustomerAsWelcomedCommand)).GetType(command.Type);
            var request = JsonConvert.DeserializeObject(command.Data, type);

            command.ProcessedDate = DateTime.UtcNow;


            await this._mediator.Send((IRequest)request);
        }
    }
}

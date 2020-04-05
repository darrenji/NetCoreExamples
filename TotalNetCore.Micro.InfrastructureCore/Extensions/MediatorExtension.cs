using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TotalNetCore.Micro.DomainAbstraction;

namespace TotalNetCore.Micro.InfrastructureCore.Extensions
{
    public static class MediatorExtension
    {
        /// <summary>
        /// 把上下文中所有热实体的所有事件发布出去
        /// 再调用工作者单元之后用
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, DbContext ctx)
        {
            //获取上下文中热具有事件的热Entity
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomanEvents != null && x.Entity.DomanEvents.Any());

            //获取所有热Entity中的所有事件
            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomanEvents)
                .ToList();

            //发布之前把所有的事件清空
            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            //把上下文的所有热实体的所有事件发布出去
            foreach(var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent);
            }
        }
    }
}

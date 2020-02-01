using Autofac;
using Autofac.Core;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TotalNetCore.DDDAPISample.Application.Configuration.DomainEvents;
using TotalNetCore.DDDAPISample.Domain.Interfaces;
using TotalNetCore.DDDAPISample.Domain.Shared;
using TotalNetCore.DDDAPISample.Infrastructure.Database;
using TotalNetCore.DDDAPISample.Infrastructure.Processing.Outbox;

namespace TotalNetCore.DDDAPISample.Infrastructure.Processing
{
    /// <summary>
    /// 发送领域事件的实现
    /// 不仅把领域事件通过MediatorR发布出去，而且把Domain Notificaiton Object持久化到了数据库
    /// </summary>
    public class DomainEventsDispatcher : IDomainEventsDispatcher
    {
        private readonly IMediator _mediator;//中介者模式MediatR的IMediator接口
        private readonly ILifetimeScope _scope;//Autofac的生命周期接口，内含委托
        private readonly OrdersContext _ordersContext;//其实是Read Model的时候用到的上下文

        public DomainEventsDispatcher(IMediator mediator, ILifetimeScope scope, OrdersContext ordersContext)
        {
            this._mediator = mediator;
            this._scope = scope;
            this._ordersContext = ordersContext;
        }

        public async Task DispatchEventsAsync()
        {

            /*
             通过所有领域的基类Entity,找到所有的领域事件IDomainEvent。

            IDomainEvent是什么呢？
             --是MediatR的INotification接口。而且INotification接口没有任何方法，只是表示MediatR的一个接口

            Entity中的IDomainEvent集合时如何把元素加入其中呢？
            --是通过领域方法，在领域方法中把所有的时间汇聚到Entity中的类型为IDomainEvent的这个集合里来
            --然后又IDomainEvent的实现基类DomainEventBase,所有的领域事件再实现DomainEventBase这个基类

             */

            //找到具备领域事件IDomainEvent的领域
            var domainEntities = this._ordersContext.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

            //找到所有的领域事件
            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            //IDomainEventNotification<>是Domain Notification Object的接口，是Outbox Pattern的一种写法
            //IDomainEvent是所有领域事件的接口，本质上是MediatR的INotification接口
            var domainEventNotifications = new List<IDomainEventNotification<IDomainEvent>>();
            foreach (var domainEvent in domainEvents)//遍历领域事件，即便利IDomainEvent的集合
            {
                Type domainEvenNotificationType = typeof(IDomainEventNotification<>);//找到Domain Notification Object，即IDomainEventNotification接口的Type实例
                var domainNotificationWithGenericType = domainEvenNotificationType.MakeGenericType(domainEvent.GetType());//找出泛型类型为IDomainEvent的Type实例
                var domainNotification = _scope.ResolveOptional(domainNotificationWithGenericType, new List<Parameter>
                {
                    new NamedParameter("domainEvent", domainEvent)//注册的时候带参数，解析的时候也带参数
                });//解析出IDomainEventNotification<IDomainEvent>实例

                if (domainNotification != null)
                {
                    domainEventNotifications.Add(domainNotification as IDomainEventNotification<IDomainEvent>);
                }
            }

            //List<IDomainEventNotification<IDomainEvent>>加载到内存后，清空所有领域内的事件
            domainEntities
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            var tasks = domainEvents
                .Select(async (domainEvent) =>
                {
                    //借助MedatorR把IDomainNotfication<IDomainEvent>发布出去
                    await _mediator.Publish(domainEvent);
                });

            await Task.WhenAll(tasks);

            foreach (var domainEventNotification in domainEventNotifications)//遍历List<IDomainEventNotification<IDomainEvent>>
            {
                string type = domainEventNotification.GetType().FullName;
                var data = JsonConvert.SerializeObject(domainEventNotification);
                OutboxMessage outboxMessage = new OutboxMessage(
                    domainEventNotification.DomainEvent.OccurredOn,
                    type,
                    data);
                this._ordersContext.OutboxMessages.Add(outboxMessage);
            }
        }
    }
}

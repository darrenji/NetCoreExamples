using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.CoffeeDDD.Common;
using TotalNetCore.CoffeeDDD.Common.EventHandlers;

namespace TotalNetCore.CoffeeDDD.RoastPlanning.Tests
{
    public abstract class EventSpecification<TAggregateRoot,TCommand>
        where TAggregateRoot : AggregateRoot,new()
        where TCommand : Command
    {
        protected virtual IEnumerable<Event> Given()
        {
            return new List<Event>();
        }

        protected abstract TCommand When();
        protected abstract ICommandHandler<TCommand> CommandHandler();

        //Aggregate肯定需要
        protected TAggregateRoot AggregateRoot;

        //过程中必定经过Repository: Command → CommandHandler → Repository → EventStore → EventPublisher
        protected Mock<IRepository<TAggregateRoot>> MockRepository;

        //collects published events for assertion
        protected IEnumerable<Event> PublishedEvents;
        protected Exception CauhgtException;

        protected EventSpecification()
        {
            AggregateRoot = new TAggregateRoot();

            //让Aggregate所有的事件都执行一遍，达到某个状态
            AggregateRoot.LoadFromHistory(Given());

            MockRepository = new Mock<IRepository<TAggregateRoot>>();
            MockRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(AggregateRoot);
            MockRepository.Setup(x => x.Save(It.IsAny<TAggregateRoot>(), It.IsAny<int>()))
                .Callback<TAggregateRoot, int>((x,_) => AggregateRoot = x);

            try
            {
                CommandHandler().Handle(When());
                PublishedEvents = new List<Event>(AggregateRoot.GetUncommittedChanges());
            }
            catch (Exception exception)
            {
                CauhgtException = exception;
            }
        }
    }
}

using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TotalNetCore.CoffeeDDD.Common;
using TotalNetCore.CoffeeDDD.Common.EventHandlers;
using TotalNetCore.CoffeeDDD.RoastPlanning.CommandHandlers;
using TotalNetCore.CoffeeDDD.RoastPlanning.Commands;
using TotalNetCore.CoffeeDDD.RoastPlanning.Events;
using TotalNetCore.CoffeeDDD.RoastPlanning.Models;

namespace TotalNetCore.CoffeeDDD.RoastPlanning.Tests.Scenerarios.Adding_a_new_roast_schedule
{
    public class When_starting_to_create_a_new_roast_schedule : EventSpecification<RoastSchedule, CreateNewRoastScheduleCommand>
    {
        private readonly Guid Id;

        public When_starting_to_create_a_new_roast_schedule()
        {
            Id = Guid.NewGuid();
        }

        protected override ICommandHandler<CreateNewRoastScheduleCommand> CommandHandler()
        {
            return new RoastScheduleCommandHandlers(MockRepository.Object);
        }

        protected override CreateNewRoastScheduleCommand When()
        {
            return new CreateNewRoastScheduleCommand(Id);
        }

        [Then]
        public void Then_a_roast_schedule_created_event_will_be_published()
        {
            PublishedEvents.Last().As<RoastScheduleCreatedEvent>().Id.Should().Be(Id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.CoffeeDDD.Common;
using TotalNetCore.CoffeeDDD.RoastPlanning.Commands;
using TotalNetCore.CoffeeDDD.RoastPlanning.Models;
using TotalNetCore.CoffeeDDD.RoastPlanning.Services;

namespace TotalNetCore.CoffeeDDD.RoastPlanning.CommandHandlers
{
    public class RoastScheduleCommandHandlers : ICommandHandler<CreateNewRoastScheduleCommand>, ICommandHandler<ChooseRoastDaysForRoastScheduleCommand>
    {
        private readonly IRepository<RoastSchedule> _repository;

        public RoastScheduleCommandHandlers(IRepository<RoastSchedule> repository)
        {
            _repository = repository;
        }

        public void Handle(CreateNewRoastScheduleCommand message)
        {
            var roastWeekStartsOn = DateTime.Now.StartOfWeek(DayOfWeek.Sunday);
            var roastSchedule = new RoastSchedule(message.RoastScheduleId, roastWeekStartsOn);
            _repository.Save(roastSchedule,-1);
        }

        public void Handle(ChooseRoastDaysForRoastScheduleCommand message)
        {
            var roastSchedule = _repository.GetById(message.Id);
            roastSchedule.SetRoastDays(new RoastDays(message.RoastDays));
            _repository.Save(roastSchedule, message.OriginalVersion);
        }
    }
}

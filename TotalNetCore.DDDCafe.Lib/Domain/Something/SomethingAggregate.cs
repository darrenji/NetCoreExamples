using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.DDDCafe.Lib.CQRS;
using TotalNetCore.DDDCafe.Lib.Events.Something;

namespace TotalNetCore.DDDCafe.Lib.Domain.Something
{
    public class SomethingAggregate : Aggregate, IHandleCommand<MakeSomethingHappen>, IApplyEvent<SomethingHappened>
    {
        private bool alreadyHappened;
        public void Apply(SomethingHappened e)
        {
            alreadyHappened = true;
        }

        public IEnumerable<Event> Handle(MakeSomethingHappen command)
        {
            if (alreadyHappened)
                throw new SomethingCanOnlyHappenOnce();

            yield return new SomethingHappened
            {
                Id = command.Id,
                What = command.What
            };
        }
    }
}

using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.DDDGuestbook.Core.Events;
using TotalNetCore.DDDGuestbook.Core.Interfaces;

namespace TotalNetCore.DDDGuestbook.Core.Handlers
{
    public class ItemCompletedEmailNotificationHandler : IHandle<ToDoItemCompletedEvent>
    {
        public void Handle(ToDoItemCompletedEvent domainEvent)
        {
            Guard.Against.Null(domainEvent, nameof(domainEvent));

            // Do Nothing
        }
    }
}

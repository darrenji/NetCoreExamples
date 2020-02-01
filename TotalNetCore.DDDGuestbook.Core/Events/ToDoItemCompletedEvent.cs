using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.DDDGuestbook.Core.Entities;
using TotalNetCore.DDDGuestbook.Core.SharedKernel;

namespace TotalNetCore.DDDGuestbook.Core.Events
{
    public class ToDoItemCompletedEvent : BaseDomainEvent
    {
        public ToDoItem CompletedItem { get; set; }

        public ToDoItemCompletedEvent(ToDoItem completedItem)
        {
            CompletedItem = completedItem;
        }
    }
}

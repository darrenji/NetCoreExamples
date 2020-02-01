using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.DDDGuestbook.Core.Entities;
using TotalNetCore.DDDGuestbook.Core.SharedKernel;

namespace TotalNetCore.DDDGuestbook.Core.Events
{
    public class EntryAddedEvent : BaseDomainEvent
    {
        public int GuestbookId { get; }
        public GuestbookEntry Entry { get; }

        public EntryAddedEvent(int guestbookId, GuestbookEntry entry)
        {
            GuestbookId = guestbookId;
            Entry = entry;
        }
    }
}

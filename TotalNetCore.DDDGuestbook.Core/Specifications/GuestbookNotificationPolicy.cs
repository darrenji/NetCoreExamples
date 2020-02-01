using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using TotalNetCore.DDDGuestbook.Core.Entities;
using TotalNetCore.DDDGuestbook.Core.Interfaces;

namespace TotalNetCore.DDDGuestbook.Core.Specifications
{
    public class GuestbookNotificationPolicy : ISpecification<GuestbookEntry>
    {
        public Expression<Func<GuestbookEntry, bool>> Criteria { get; }

        public GuestbookNotificationPolicy(int entryAddedId = 0)
        {
            Criteria = e => e.DateTimeCreated > DateTime.UtcNow.AddDays(-1) && e.Id != entryAddedId;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TotalNetCore.DDDGuestbook.Core.Events;
using TotalNetCore.DDDGuestbook.Core.SharedKernel;

namespace TotalNetCore.DDDGuestbook.Core.Entities
{
    public class Guestbook : BaseEntity
    {
        private readonly List<GuestbookEntry> _entries = new List<GuestbookEntry>();

        public IEnumerable<GuestbookEntry> Entries
        {
            get { return new ReadOnlyCollection<GuestbookEntry>(_entries); }
        }

        public string Name { get; set; }

        public void AddEntry(GuestbookEntry entry)
        {
            _entries.Add(entry);
            Events.Add(new EntryAddedEvent(this.Id, entry));
        }
    }
}

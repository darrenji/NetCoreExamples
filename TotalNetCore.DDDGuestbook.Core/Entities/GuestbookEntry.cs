using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.DDDGuestbook.Core.SharedKernel;

namespace TotalNetCore.DDDGuestbook.Core.Entities
{
    public class GuestbookEntry : BaseEntity
    {
        public string EmailAddress { get; set; }
        public string Message { get; set; }
        public DateTime DateTimeCreated { get; set; } = DateTime.UtcNow;
    }
}

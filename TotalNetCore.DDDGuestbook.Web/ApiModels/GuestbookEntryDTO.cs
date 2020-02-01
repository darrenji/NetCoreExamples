using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotalNetCore.DDDGuestbook.Web.ApiModels
{
    public class GuestbookEntryDTO
    {
        public int Id { get; set; }
        public string EmailAddress { get; set; }
        public string Message { get; set; }
        public DateTime DateTimeCreated { get; set; } = DateTime.UtcNow;
    }
}

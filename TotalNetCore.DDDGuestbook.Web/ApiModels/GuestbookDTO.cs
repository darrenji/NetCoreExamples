using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotalNetCore.DDDGuestbook.Web.ApiModels
{
    public class GuestbookDTO
    {
        public int Id { get; set; }
        public List<GuestbookEntryDTO> Entries { get; set; } = new List<GuestbookEntryDTO>();
    }
}

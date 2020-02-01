using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotalNetCore.DDDGuestbook.Web.ApiModels;

namespace TotalNetCore.DDDGuestbook.Web.Models
{
    public class HomePageViewModel
    {
        public string GuestbookName { get; set; }
        public List<GuestbookEntryDTO> PreviousEntries { get; } = new List<GuestbookEntryDTO>();
        public GuestbookEntryDTO NewEntry { get; set; }
    }
}

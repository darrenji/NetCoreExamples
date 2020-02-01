using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TotalNetCore.DDDGuestbook.Core.Entities;
using TotalNetCore.DDDGuestbook.Core.Interfaces;
using TotalNetCore.DDDGuestbook.Web.Filters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TotalNetCore.DDDGuestbook.Web.Api
{
    public class GuestbookController : BaseApiController
    {
        private readonly IRepository _repository;

        public GuestbookController(IRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Guestbook/1
        [HttpGet("{id:int}")]
        [VerifyGuestbookExists]
        public IActionResult GetById(int id)
        {
            var guestbook = _repository.GetById<Guestbook>(id, "Entries");
            return Ok(guestbook);
        }

        [HttpPost("{id:int}/NewEntry")]
        [VerifyGuestbookExists]
        public IActionResult NewEntry(int id, [FromBody] GuestbookEntry entry)
        {
            var guestbook = _repository.GetById<Guestbook>(id, "Entries");

            guestbook.AddEntry(entry);
            _repository.Update(guestbook);

            return Ok(guestbook);
        }
    }
}

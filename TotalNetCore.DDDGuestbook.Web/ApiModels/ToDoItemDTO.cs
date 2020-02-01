using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TotalNetCore.DDDGuestbook.Core.Entities;

namespace TotalNetCore.DDDGuestbook.Web.ApiModels
{
    public class ToDoItemDTO
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; private set; }

        public static ToDoItemDTO FromToDoItem(ToDoItem item)
        {
            return new ToDoItemDTO()
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                IsDone = item.IsDone
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TotalNetCore.DDDEventPattern
{
   public interface IUserService
    {
        Task CreateAsync(string email);
    }
}

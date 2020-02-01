using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotalNetCore.AutofacExample.Web.Services
{
    public interface IValuesService
    {
        IEnumerable<string> FindAll();
        String Find(int id);
    }
}

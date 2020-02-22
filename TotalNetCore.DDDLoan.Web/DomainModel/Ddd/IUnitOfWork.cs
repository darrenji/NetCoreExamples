using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotalNetCore.DDDLoan.Web.DomainModel.Ddd
{
   public  interface IUnitOfWork
    {
        void CommitChanges();
    }
}

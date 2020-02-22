using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotalNetCore.DDDLoan.Web.DomainModel.Ddd;

namespace TotalNetCore.DDDLoan.Web.Persistence
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly LoanDbContext dbContext;

        public EfUnitOfWork(LoanDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void CommitChanges()
        {
            dbContext.SaveChanges();
        }
    }
}

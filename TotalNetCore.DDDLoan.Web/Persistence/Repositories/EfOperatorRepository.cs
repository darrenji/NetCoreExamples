using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotalNetCore.DDDLoan.Web.DomainModel;

namespace TotalNetCore.DDDLoan.Web.Persistence.Repositories
{
    public class EfOperatorRepository : IOperatorRepository
    {
        private readonly LoanDbContext dbContext;

        public EfOperatorRepository(LoanDbContext dbContext)
        {
            this.dbContext = dbContext;   
        }
        public void Add(Operator @operator)
        {
            dbContext.Operators.Add(@operator);
        }

        public Operator WithLogin(string login)
        {
            return dbContext.Operators.FirstOrDefault(t => t.Login == login);
        }
    }
}

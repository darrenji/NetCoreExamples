using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotalNetCore.DDDLoan.Web.DomainModel;

namespace TotalNetCore.DDDLoan.Web.Persistence
{
    public class EfLoanApplicationRepository : ILoanApplicationRepository
    {
        private readonly LoanDbContext dbContext;

        public EfLoanApplicationRepository(LoanDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(LoanApplication loanApplication)
        {
            dbContext.LoanApplications.Add(loanApplication);
        }

        public LoanApplication WithNumber(string loanApplicationNumber)
        {
            return dbContext.LoanApplications.FirstOrDefault(t => t.Number == loanApplicationNumber);
        }
    }
}

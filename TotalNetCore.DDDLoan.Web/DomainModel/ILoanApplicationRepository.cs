using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotalNetCore.DDDLoan.Web.DomainModel
{
    public interface ILoanApplicationRepository
    {
        void Add(LoanApplication loanApplication);

        LoanApplication WithNumber(string loanApplicationNumber);
    }
}

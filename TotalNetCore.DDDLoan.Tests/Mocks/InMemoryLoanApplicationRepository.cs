using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TotalNetCore.DDDLoan.Web.DomainModel;

namespace TotalNetCore.DDDLoan.Tests.Mocks
{
    public class InMemoryLoanApplicationRepository : ILoanApplicationRepository
    {
        private readonly ConcurrentDictionary<LoanApplicationId, LoanApplication> applications =
            new ConcurrentDictionary<LoanApplicationId,LoanApplication>();

        public InMemoryLoanApplicationRepository(IEnumerable<LoanApplication> initialData)
        {
            foreach (var application in initialData)
            {
                applications[application.Id] = application;
            }
        }
        public void Add(LoanApplication loanApplication)
        {
            applications[loanApplication.Id] = loanApplication;
        }

        public LoanApplication WithNumber(string loanApplicationNumber)
        {
            return applications.Values.FirstOrDefault(a => a.Number == loanApplicationNumber);
        }
    }
}

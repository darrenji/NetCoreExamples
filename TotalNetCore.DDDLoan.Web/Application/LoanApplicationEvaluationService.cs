using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotalNetCore.DDDLoan.Web.DomainModel;
using TotalNetCore.DDDLoan.Web.DomainModel.Ddd;

namespace TotalNetCore.DDDLoan.Web.Application
{
    public class LoanApplicationEvaluationService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILoanApplicationRepository loanApplications;
        private readonly IDebtorRegistry debtorRegistry;
        private readonly ScoringRulesFactory scoringRulesFactory;

        public LoanApplicationEvaluationService(IUnitOfWork unitOfWork, ILoanApplicationRepository loanApplications, IDebtorRegistry debtorRegistry)
        {
            this.unitOfWork = unitOfWork;
            this.loanApplications = loanApplications;
            this.debtorRegistry = debtorRegistry;
            this.scoringRulesFactory = new ScoringRulesFactory(debtorRegistry);
        }
        public void EvaluateLoanApplication(string applicationNumber)
        {
            var loanApplication = loanApplications.WithNumber(applicationNumber);

            loanApplication.Evaluate(scoringRulesFactory.DefaultSet);

            unitOfWork.CommitChanges();
        }
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TotalNetCore.DDDLoan.Web.DomainModel;
using TotalNetCore.DDDLoan.Web.DomainModel.Ddd;

namespace TotalNetCore.DDDLoan.Web.Application
{
    public static class EvaluateLoanApplication
    {
        public class Command : IRequest<Unit>
        {
            public string ApplicationNumber { get; set; }
        }


        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly IUnitOfWork unitOfWork;
            private readonly ILoanApplicationRepository loanApplications;
            private readonly IDebtorRegistry debtorRegistry;
            private readonly ScoringRulesFactory scoringRulesFactory;

            public Handler(IUnitOfWork unitOfWork, ILoanApplicationRepository loanApplications, IDebtorRegistry debtorRegistry)
            {
                this.unitOfWork = unitOfWork;
                this.loanApplications = loanApplications;
                this.debtorRegistry = debtorRegistry;
                this.scoringRulesFactory = new ScoringRulesFactory(debtorRegistry);
            }

            public Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var loanApplication = loanApplications.WithNumber(request.ApplicationNumber);

                loanApplication.Evaluate(scoringRulesFactory.DefaultSet);

                unitOfWork.CommitChanges();

                return Task.FromResult(Unit.Value);
            }
        }
    }
}

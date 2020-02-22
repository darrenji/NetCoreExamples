﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TotalNetCore.DDDLoan.Web.DomainModel;
using TotalNetCore.DDDLoan.Web.DomainModel.Ddd;
using TotalNetCore.DDDLoan.Web.DomainModel.DomainEvents;

namespace TotalNetCore.DDDLoan.Web.Application
{
    public class LoanApplicationDecisionService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILoanApplicationRepository loanApplications;
        private readonly IOperatorRepository operators;
        private readonly IEventPublisher eventPublisher;

        public LoanApplicationDecisionService(
            IUnitOfWork unitOfWork,
            ILoanApplicationRepository loanApplications,
            IOperatorRepository operators,
            IEventPublisher eventPublisher)
        {
            this.unitOfWork = unitOfWork;
            this.loanApplications = loanApplications;
            this.operators = operators;
            this.eventPublisher = eventPublisher;
        }

        public void RejectApplication(string applicationNumber, ClaimsPrincipal principal, string rejectionReason)
        {
            var loanApplication = loanApplications.WithNumber(applicationNumber);
            var user = operators.WithLogin(principal.Identity.Name);

            loanApplication.Reject(user);

            unitOfWork.CommitChanges();

            eventPublisher.Publish(new LoanApplicationRejected(loanApplication));
        }

        public void AcceptApplication(string applicationNumber, ClaimsPrincipal principal)
        {
            var loanApplication = loanApplications.WithNumber(applicationNumber);
            var user = operators.WithLogin(principal.Identity.Name);

            loanApplication.Accept(user);

            unitOfWork.CommitChanges();

            eventPublisher.Publish(new LoanApplicationAccepted(loanApplication));
        }
    }
}

﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using TotalNetCore.DDDLoan.Web.DomainModel;
using TotalNetCore.DDDLoan.Web.DomainModel.Ddd;
using TotalNetCore.DDDLoan.Web.DomainModel.DomainEvents;

namespace TotalNetCore.DDDLoan.Web.Application
{
    public static class RejectLoanApplication
    {
        public class Command : IRequest<Unit>
        {
            public string ApplicationNumber { get; set; }

            public ClaimsPrincipal CurrentUser { get; set; }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly IUnitOfWork unitOfWork;
            private readonly ILoanApplicationRepository loanApplications;
            private readonly IOperatorRepository operators;
            private readonly IEventPublisher eventPublisher;

            public Handler(
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

            public Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var loanApplication = loanApplications.WithNumber(request.ApplicationNumber);
                var user = operators.WithLogin(request.CurrentUser.Identity.Name);

                loanApplication.Reject(user);

                unitOfWork.CommitChanges();

                eventPublisher.Publish(new LoanApplicationRejected(loanApplication));

                return Task.FromResult(Unit.Value);
            }
        }
    }
}

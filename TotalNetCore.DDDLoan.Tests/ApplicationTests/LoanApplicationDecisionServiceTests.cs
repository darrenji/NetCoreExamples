using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using TotalNetCore.DDDLoan.Tests.Asserts;
using TotalNetCore.DDDLoan.Tests.Builders;
using TotalNetCore.DDDLoan.Tests.Mocks;
using TotalNetCore.DDDLoan.Web.Application;
using TotalNetCore.DDDLoan.Web.DomainModel;
using TotalNetCore.DDDLoan.Web.DomainModel.DomainEvents;
using Xunit;

namespace TotalNetCore.DDDLoan.Tests.ApplicationTests
{
    public class LoanApplicationDecisionServiceTests
    {
        [Fact]
        public void LoanApplicationDecisionService_GreenApplication_CanBeAccepted()
        {
            var operators = new InMemoryOperatorRepository(new List<Operator>
            {
                new OperatorBuilder().WithLogin("admin").Build()
            });

            var existingApplications = new InMemoryLoanApplicationRepository(new[]
            {
                new LoanApplicationBuilder()
                    .WithNumber("123")
                    .WithCustomer(customer => customer.WithAge(25).WithIncome(15_000M))
                    .WithLoan(loan => loan.WithAmount(200_000).WithNumberOfYears(25).WithInterestRate(1.1M))
                    .WithProperty(prop => prop.WithValue(250_000M))
                    .Evaluated()
                    .Build()
            });

            var eventBus = new InMemoryBus();

            var decisionService = new LoanApplicationDecisionService
            (
                new UnitOfWorkMock(),
                existingApplications,
                operators,
                eventBus
            );


            decisionService.AcceptApplication("123", OperatorIdentity("admin"));

            LoanApplicationAssert
                .That(existingApplications.WithNumber("123"))
                .IsInStatus(LoanApplicationStatus.Accepted);

            DomainEventsAssert
                .That(eventBus.Events)
                .HasExpectedNumberOfEvents(1)
                .ContainsEvent<LoanApplicationAccepted>(e => e.LoanApplicationId == existingApplications.WithNumber("123").Id.Value);
        }

        [Fact]
        public void LoanApplicationDecisionService_GreenApplication_CanBeRejected()
        {
            var operators = new InMemoryOperatorRepository(new List<Operator>
            {
                new OperatorBuilder().WithLogin("admin").Build()
            });

            var existingApplications = new InMemoryLoanApplicationRepository(new[]
            {
                new LoanApplicationBuilder()
                    .WithNumber("123")
                    .WithCustomer(customer => customer.WithAge(25).WithIncome(15_000M))
                    .WithLoan(loan => loan.WithAmount(200_000).WithNumberOfYears(25).WithInterestRate(1.1M))
                    .WithProperty(prop => prop.WithValue(250_000M))
                    .Evaluated()
                    .Build()
            });

            var eventBus = new InMemoryBus();

            var decisionService = new LoanApplicationDecisionService
            (
                new UnitOfWorkMock(),
                existingApplications,
                operators,
                eventBus
            );


            decisionService.RejectApplication("123", OperatorIdentity("admin"), null);

            LoanApplicationAssert
                .That(existingApplications.WithNumber("123"))
                .IsInStatus(LoanApplicationStatus.Rejected);

            DomainEventsAssert
                .That(eventBus.Events)
                .HasExpectedNumberOfEvents(1)
                .ContainsEvent<LoanApplicationRejected>(e => e.LoanApplicationId == existingApplications.WithNumber("123").Id.Value);
        }

        private ClaimsPrincipal OperatorIdentity(string login)
        {
            return new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, login)
            }));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.DDDLoan.Tests.Asserts;
using TotalNetCore.DDDLoan.Tests.Builders;
using TotalNetCore.DDDLoan.Tests.Mocks;
using TotalNetCore.DDDLoan.Web.Application;
using TotalNetCore.DDDLoan.Web.DomainModel;
using Xunit;

namespace TotalNetCore.DDDLoan.Tests.ApplicationTests
{
    public class LoanApplicationEvaluationServiceTests
    {
        [Fact]
        public void LoanApplicationEvaluationService_ApplicationThatSatisfiesAllRules_IsEvaluatedGreen()
        {
            var existingApplications = new InMemoryLoanApplicationRepository(new[]
            {
                new LoanApplicationBuilder()
                    .WithNumber("123")
                    .WithCustomer(customer => customer.WithAge(25).WithIncome(15_000M))
                    .WithLoan(loan => loan.WithAmount(200_000).WithNumberOfYears(25).WithInterestRate(1.1M))
                    .WithProperty(prop => prop.WithValue(250_000M))
                    .Build()
            });

            var evaluationService = new LoanApplicationEvaluationService
            (
                new UnitOfWorkMock(),
                existingApplications,
                new DebtorRegistryMock()
            );

            evaluationService.EvaluateLoanApplication("123");

            LoanApplicationAssert
                .That(existingApplications.WithNumber("123"))
                .ScoreIs(ApplicationScore.Green);
        }

        [Fact]
        public void LoanApplicationEvaluationService_ApplicationThatDoesNotSatisfyAllRules_IsEvaluatedRedAndRejected()
        {
            var existingApplications = new InMemoryLoanApplicationRepository(new[]
            {
                new LoanApplicationBuilder()
                    .WithNumber("123")
                    .WithCustomer(customer => customer.WithAge(55).WithIncome(15_000M))
                    .WithLoan(loan => loan.WithAmount(200_000).WithNumberOfYears(25).WithInterestRate(1.1M))
                    .WithProperty(prop => prop.WithValue(250_000M))
                    .Build()
            });

            var evaluationService = new LoanApplicationEvaluationService
            (
                new UnitOfWorkMock(),
                existingApplications,
                new DebtorRegistryMock()
            );

            evaluationService.EvaluateLoanApplication("123");

            LoanApplicationAssert
                .That(existingApplications.WithNumber("123"))
                .ScoreIs(ApplicationScore.Red)
                .IsInStatus(LoanApplicationStatus.Rejected);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.DDDLoan.Web.DomainModel;
using Xunit;

namespace TotalNetCore.DDDLoan.Tests.Asserts
{
    public class LoanApplicationAssert
    {
        private readonly LoanApplication loanApplication;

        public LoanApplicationAssert(LoanApplication loanApplication)
        {
            this.loanApplication = loanApplication;
        }

        public static LoanApplicationAssert That(LoanApplication loanApplication) => new LoanApplicationAssert(loanApplication);

        public LoanApplicationAssert IsInStatus(LoanApplicationStatus expectedStaus)
        {
            Assert.Equal(expectedStaus, loanApplication.Status);
            return this;
        }

        public LoanApplicationAssert ScoreIsNull()
        {
            Assert.Null(loanApplication.Score);
            return this;
        }

        public LoanApplicationAssert ScoreIs(ApplicationScore expectedScore)
        {
            Assert.Equal(expectedScore, loanApplication.Score?.Score);
            return this;
        }
    }
}

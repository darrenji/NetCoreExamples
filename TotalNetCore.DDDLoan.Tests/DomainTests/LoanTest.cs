﻿using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.DDDLoan.Tests.Builders;
using TotalNetCore.DDDLoan.Web.DomainModel;
using Xunit;

namespace TotalNetCore.DDDLoan.Tests.DomainTests
{
    public class LoanTest
    {
        [Fact]
        public void Can_calculate_monthly_installment()
        {
            var loan = new LoanBuilder()
                .WithAmount(420_000M)
                .WithNumberOfYears(3)
                .WithInterestRate(5M)
                .Build();

            var installment = loan.MonthlyInstallment();

            Assert.Equal(new MonetaryAmount(12_587.78M), installment);
        }
    }
}

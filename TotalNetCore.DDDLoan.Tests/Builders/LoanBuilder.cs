﻿using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.DDDLoan.Web.DomainModel;

namespace TotalNetCore.DDDLoan.Tests.Builders
{
   public  class LoanBuilder
    {
        private MonetaryAmount amount = new MonetaryAmount(200_000M);
        private int numberOfYears = 20;
        private Percent interestRate = 1.Percent();

        public LoanBuilder WithAmount(decimal loanAmount)
        {
            amount = new MonetaryAmount(loanAmount);
            return this;
        }

        public LoanBuilder WithNumberOfYears(int numOfYears)
        {
            numberOfYears = numOfYears;
            return this;
        }

        public LoanBuilder WithInterestRate(decimal rate)
        {
            interestRate = new Percent(rate);
            return this;
        }

        public Loan Build()
        {
            return new Loan(amount, numberOfYears, interestRate);
        }
    }
}

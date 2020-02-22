﻿using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.DDDLoan.Tests.Mocks;
using TotalNetCore.DDDLoan.Web.DomainModel;

namespace TotalNetCore.DDDLoan.Tests.Builders
{
    public class LoanApplicationBuilder
    {
        private Operator user = new Operator("admin", "admin", "admin", "admin", new MonetaryAmount(1_000_000));
        private Customer customer = new CustomerBuilder().Build();
        private Property property = new PropertyBuilder().Build();
        private Loan loan = new LoanBuilder().Build();
        private string applicationNumber = Guid.NewGuid().ToString();
        private bool evaluated = false;
        private LoanApplicationStatus targetStatus = LoanApplicationStatus.New;
        private ScoringRulesFactory scoringRulesFactory = new ScoringRulesFactory(new DebtorRegistryMock());

        public LoanApplicationBuilder Accepted()
        {
            targetStatus = LoanApplicationStatus.Accepted;
            return this;
        }

        public LoanApplicationBuilder Rejected()
        {
            targetStatus = LoanApplicationStatus.Rejected;
            return this;
        }

        public LoanApplicationBuilder Evaluated()
        {
            evaluated = true;
            return this;
        }

        public LoanApplicationBuilder NotEvaluated()
        {
            evaluated = false;
            return this;
        }

        public LoanApplicationBuilder WithNumber(string number)
        {
            applicationNumber = number;
            return this;
        }

        public LoanApplicationBuilder WithOperator(string login)
        {
            user = new Operator(login, login, login, login, new MonetaryAmount(1_000_000));
            return this;
        }

        public LoanApplicationBuilder WithCustomer(Action<CustomerBuilder> customizeCustomer)
        {
            var customerBuilder = new CustomerBuilder();
            customizeCustomer(customerBuilder);
            customer = customerBuilder.Build();
            return this;
        }

        public LoanApplicationBuilder WithProperty(Action<PropertyBuilder> propertyCustomizer)
        {
            var propertyBuilder = new PropertyBuilder();
            propertyCustomizer(propertyBuilder);
            property = propertyBuilder.Build();
            return this;
        }

        public LoanApplicationBuilder WithLoan(Action<LoanBuilder> loanCustomizer)
        {
            var loanBuilder = new LoanBuilder();
            loanCustomizer(loanBuilder);
            loan = loanBuilder.Build();
            return this;
        }

        public LoanApplication Build()
        {
            var application = new LoanApplication
            (
                applicationNumber,
                customer,
                property,
                loan,
                user
            );

            if (evaluated)
            {
                application.Evaluate(scoringRulesFactory.DefaultSet);
            }

            if (targetStatus == LoanApplicationStatus.Accepted)
            {
                application.Accept(user);
            }

            if (targetStatus == LoanApplicationStatus.Rejected)
            {
                application.Reject(user);
            }

            return application;
        }
    }
}

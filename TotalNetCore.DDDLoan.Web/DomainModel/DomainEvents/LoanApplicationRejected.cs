﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotalNetCore.DDDLoan.Web.DomainModel.Ddd;

namespace TotalNetCore.DDDLoan.Web.DomainModel.DomainEvents
{
    public class LoanApplicationRejected : DomainEvent
    {
        public Guid LoanApplicationId { get; }

        public LoanApplicationRejected(LoanApplication loanApplication)
            : this(loanApplication.Id.Value)
        {
        }

        [JsonConstructor]
        protected LoanApplicationRejected(Guid id)
        {
            LoanApplicationId = id;
        }
    }
}

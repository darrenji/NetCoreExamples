using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotalNetCore.DDDLoan.Web.DomainModel
{
    public class ScoringRulesFactory
    {
        private readonly IDebtorRegistry debtorRegistry;

        public ScoringRulesFactory(IDebtorRegistry debtorRegistry)
        {
            this.debtorRegistry = debtorRegistry;
        }

        /// <summary>
        /// 这里的工厂并不是产生实例，而是把实例放在集合中，然后提供另外的方法使用这些内在的实例集合
        /// </summary>
        public ScoringRules DefaultSet => new ScoringRules(new List<IScoringRule>
        {
            new LoanAmountMustBeLowerThanPropertyValue(),
            new CustomerAgeAtTheDateOfLastInstallmentMustBeBelow65(),
            new InstallmentAmountMustBeLowerThen15PercentOfCustomerIncome(),
            new CustomerIsNotARegisteredDebtor(debtorRegistry)
        });
    }
}

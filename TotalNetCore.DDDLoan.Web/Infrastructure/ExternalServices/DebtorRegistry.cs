using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotalNetCore.DDDLoan.Web.DomainModel;

namespace TotalNetCore.DDDLoan.Web.Infrastructure.ExternalServices
{
    public class DebtorRegistry : IDebtorRegistry
    {
        public bool IsRegisteredDebtor(Customer customer)
        {
            var client = new DebtorRegistoryClient();
            var debtorInfo = client.GetDebtorInfo(customer.NationalIdentifier.Value).Result;
            return debtorInfo.Debts.Any();
        }
    }
}

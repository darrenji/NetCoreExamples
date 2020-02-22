using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.DDDLoan.Web.DomainModel;

namespace TotalNetCore.DDDLoan.Tests.Mocks
{
    public class DebtorRegistryMock : IDebtorRegistry
    {
        public const string DebtorNationalIdentifier = "11111111116";
        public bool IsRegisteredDebtor(Customer customer)
        {
            return customer.NationalIdentifier == new NationalIdentifier(DebtorNationalIdentifier);
        }
    }
}

using RestEase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotalNetCore.DDDLoan.Web.Infrastructure.ExternalServices
{
    public interface IDebtorRegistryService
    {
        [Get("{pesel}")]
        Task<DebtorInfo> Get([Path]string pesel);
    }

    public class Debt
    {
        public decimal Amount { get; set; }
    }

    public class DebtorInfo
    {
        public string Pesel { get; set; }
        public List<Debt> Debts { get; set; }
    }

    public class DebtorRegistoryClient
    {
        public async Task<DebtorInfo> GetDebtorInfo(string pesel)
        {
            return await RestClient.For<IDebtorRegistryService>("http://localhost:5005/DebtorInfo").Get(pesel);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotalNetCore.DDDLoan.Web.Application.Dtos;

namespace TotalNetCore.DDDLoan.Web.ReadModel
{
    public class LoanApplicationFinder
    {
        private readonly string connectionString;

        public LoanApplicationFinder(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IList<LoanApplicationInfoDto> FindLoadApplication(LoanApplicationSearchCriteriaDto criteria)
        {
            return new List<LoanApplicationInfoDto>();
        }

        public LoanApplicationDto GetLoanApplication(string applicationNumber)
        {
            return null;
        }
    }
}

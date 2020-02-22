using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotalNetCore.DDDLoan.Web.Application.Dtos
{
    public class LoanApplicationSearchCriteriaDto
    {
        public string ApplicationNumber { get; set; }
        public string CustomerNationalIdentifier { get; set; }
        public string DecisionBy { get; set; }
        public string RegisteredBy { get; set; }
    }
}

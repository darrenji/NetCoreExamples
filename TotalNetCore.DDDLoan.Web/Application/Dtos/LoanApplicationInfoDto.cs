using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotalNetCore.DDDLoan.Web.Application.Dtos
{
    public class LoanApplicationInfoDto
    {
        public string Number { get; set; }
        public string Status { get; set; }
        public string CustomerName { get; set; }
        public DateTime? DecisionDate { get; set; }
        public decimal LoanAmount { get; set; }
        public string DecisionBy { get; set; }
    }
    
}

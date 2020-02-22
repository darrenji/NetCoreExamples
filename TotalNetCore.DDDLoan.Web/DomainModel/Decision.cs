using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotalNetCore.DDDLoan.Web.DomainModel.Ddd;

namespace TotalNetCore.DDDLoan.Web.DomainModel
{
    public class Decision : ValueObject<Decision>
    {
        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            yield return DecisionDate;
            yield return DecisionBy;
        }

        public DateTime DecisionDate { get; }
        public OperatorId DecisionBy { get; }


        [JsonConstructor]
        public Decision(DateTime decisionDate, OperatorId decisionBy)
        {
            DecisionDate = decisionDate;
            DecisionBy = decisionBy;
        }

        //tO Satisfy EF Core
        protected Decision() { }

        public Decision(DateTime decisionDate, Operator decisionBy):this(decisionDate, decisionBy.Id) { }
    }
}

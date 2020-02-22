using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotalNetCore.DDDLoan.Web.DomainModel.Ddd;

namespace TotalNetCore.DDDLoan.Web.DomainModel
{
    public class ScoringResult : ValueObject<ScoringResult>
    {
        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            yield return Score;
            yield return Explanation;
        }

        public ApplicationScore? Score { get; }
        public string Explanation { get; }

        private ScoringResult(ApplicationScore? score, string explanation)
        {
            Score = score;
            Explanation = explanation;
        }

        protected ScoringResult() { }

        public static ScoringResult Green()
        {
            return new ScoringResult(ApplicationScore.Green, null);
        }

        public static ScoringResult Red(string[] messages)
        {
            return new ScoringResult(ApplicationScore.Red, string.Join(Environment.NewLine, messages));
        }

        public bool IsRed()
        {
            return Score == ApplicationScore.Red;
        }
    }
}

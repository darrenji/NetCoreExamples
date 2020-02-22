using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotalNetCore.DDDLoan.Web.DomainModel.Ddd;

namespace TotalNetCore.DDDLoan.Web.DomainModel
{
    public class Operator : Entity<OperatorId>
    {
        public string Login { get; private set; }
        public string Password { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public MonetaryAmount CompetenceLevel { get; private set; }

        public Operator(string login, string password, string firstName, string lastName, MonetaryAmount competencyLevel)
        {
            Id = new OperatorId(Guid.NewGuid());
            Login = login;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            CompetenceLevel = competencyLevel;
        }

        //To satisfy EF Core
        protected Operator() { }

        public bool CanAccept(MonetaryAmount loanLoanAmount) => loanLoanAmount <= CompetenceLevel;
    }

    public class OperatorId:ValueObject<OperatorId>
    {
        public Guid Value { get; }

        public OperatorId(Guid value)
        {
            Value = value;
        }

        protected OperatorId() { }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            yield return Value;
        }

        
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotalNetCore.DDDLoan.Web.DomainModel.Ddd;

namespace TotalNetCore.DDDLoan.Web.DomainModel
{
    public class Registration : ValueObject<Registration>
    {
        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            yield return RegistrationDate;
            yield return RegisteredBy;
        }

        public DateTime RegistrationDate { get; }
        public OperatorId RegisteredBy { get; }

        [JsonConstructor]
        public Registration(DateTime registrationDate, OperatorId registeredBy)
        {
            RegistrationDate = registrationDate;
            RegisteredBy = registeredBy;
        }

        public Registration(DateTime registrationDate, Operator registeredBy) : this(registrationDate, registeredBy.Id) { }

        //To Satisfy EF Core
        protected Registration() { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotalNetCore.DDDLoan.Web.DomainModel.Ddd;

namespace TotalNetCore.DDDLoan.Web.DomainModel
{
    public class Property : ValueObject<Property>
    {
        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new List<object> { Value, Address};
        }

        public MonetaryAmount Value { get; }
        public Address Address { get; }

        public Property(MonetaryAmount value, Address address)
        {
            if (value == null)
                throw new ArgumentException("Value cannot be null");
            if (address == null)
                throw new ArgumentException("Address cannot be null");
            if (value <= MonetaryAmount.Zero)
                throw new ArgumentException("Property value must be higher than 0");

            Value = value;
            Address = address;
        }

        protected Property() { }


    }
}

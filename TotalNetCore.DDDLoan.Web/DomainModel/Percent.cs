using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotalNetCore.DDDLoan.Web.DomainModel.Ddd;

namespace TotalNetCore.DDDLoan.Web.DomainModel
{
    public class Percent : ValueObject<Percent>, IComparable<Percent>
    {
        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            yield return Value;

        }

        public decimal Value { get; }

        public Percent(decimal value)
        {
            if (value < 0)
                throw new ArgumentException("Percent value cannot be negative");
            Value = value;
        }

        public static readonly Percent Zero = new Percent(0M);

        //To satisfy EF Core
        protected Percent() { }

        public static bool operator >(Percent one, Percent two) => one.CompareTo(two) > 0;

        public static bool operator <(Percent one, Percent two) => one.CompareTo(two) < 0;

        public static bool operator >=(Percent one, Percent two) => one.CompareTo(two) >= 0;

        public static bool operator <=(Percent one, Percent two) => one.CompareTo(two) <= 0;

        public int CompareTo(Percent other)
        {
            return Value.CompareTo(other.Value);
        }
    }

    public static class PercentExtensions
    {
        public static Percent Percent(this int value) => new Percent(value);
        public static Percent Percent(this decimal value) => new Percent(value);
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using TotalNetCore.DDDLoan.Web.DomainModel.Ddd;

namespace TotalNetCore.DDDLoan.Web.DomainModel
{
    /// <summary>
    /// 多少年
    /// </summary>
    public class AgeInYears : ValueObject<AgeInYears>, IComparable<AgeInYears>
    {
        private readonly int age;

        public AgeInYears(int age)
        {
            this.age = age;
        }

        public static AgeInYears Between(DateTime start, DateTime end)
        {
            return new AgeInYears(end.Year - start.Year);
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            //使用了这个类的私有变量
            yield return age;
        }

        public static bool operator >(AgeInYears one, AgeInYears two) => one.CompareTo(two) > 0;

        public static bool operator <(AgeInYears one, AgeInYears two) => one.CompareTo(two) < 0;

        public static bool operator >=(AgeInYears one, AgeInYears two) => one.CompareTo(two) >= 0;

        public static bool operator <=(AgeInYears one, AgeInYears two) => one.CompareTo(two) <= 0;

        public int CompareTo(AgeInYears other)
        {
            return this.age.CompareTo(other.age);
        }

       
    }

    public static class AgeInYearsExtensions
    {
        public static AgeInYears Years(this int age) => new AgeInYears(age);
    }
}

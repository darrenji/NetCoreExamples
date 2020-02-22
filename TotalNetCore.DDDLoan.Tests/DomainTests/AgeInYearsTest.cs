using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.DDDLoan.Web.DomainModel;
using Xunit;

namespace TotalNetCore.DDDLoan.Tests.DomainTests
{
    public class AgeInYearsTest
    {
        [Fact]
        public void AgeInYears_PersonBorn1974_AfterBirthdateIn2019_45()
        {
            var age = AgeInYears.Between(new DateTime(1974, 6, 26), new DateTime(2019, 11, 28));
            Assert.Equal(45.Years(), age);
        }


        [Fact]
        public void AgeInYears_PersonBorn1974_BeforeBirthdateIn2019_45()
        {
            var age = AgeInYears.Between(new DateTime(1974, 6, 26), new DateTime(2019, 5, 28));

            Assert.Equal(45.Years(), age);
        }
    }
}

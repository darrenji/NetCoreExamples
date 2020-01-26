using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.UnitTest.EmployeeApp.Validation;
using Xunit;

namespace TotalNetCore.UnitTest.EmployeeTest.Validation
{
    public class AccountNumberValidationTests
    {
        private readonly AccountNumberValidation _validation;

        public AccountNumberValidationTests()
        {
            _validation = new AccountNumberValidation();
        }

        [Fact]
        public void IsValid_ValidAccountNumber_ReturnsTrue()
        {
            Assert.True(_validation.IsValid("123-4543234576-23"));
        }
    }
}

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

        //[Fact]
        //public void IsValid_AccountNumberFirstPartWrong_ReturnFalse()
        //{
        //    Assert.False(_validation.IsValid("1234-3454565676-23"));
        //}

        [Theory]
        [InlineData("1234-3454565676-23")]
        [InlineData("12-3454565676-23")]
        public void IsValid_AccountNumberFirstPartWrong_ReturnsFalse(string accountNumber)
        {
            Assert.False(_validation.IsValid(accountNumber));
        }

        [Theory]
        [InlineData("123-345456567-23")]
        [InlineData("123-345456567633-23")]
        public void IsValid_AccountNumberMiddlePartWrong_ReturnFalse(string accountNumber)
        {
            Assert.False(_validation.IsValid(accountNumber));
        }

        [Theory]
        [InlineData("123-3434545656-2")]
        [InlineData("123-3454565676-233")]
        public void IsValid_AccountNumberLastPartWrong_ReturnFalse(string accNumber)
        {
            Assert.False(_validation.IsValid(accNumber));
        }

        [Theory]
        [InlineData("123-345456567633=23")]
        [InlineData("123+345456567633-23")]
        [InlineData("123+345456567633=23")]
        public void IsValid_InvalidDelimiters_ThrowsArgumentException(string accNumber)
        {
            Assert.Throws<ArgumentException>(()=> _validation.IsValid(accNumber));
        }
    }
}

using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.DDDLoan.Web.DomainModel;
using Xunit;
using Property = TotalNetCore.DDDLoan.Web.DomainModel.Property;

namespace TotalNetCore.DDDLoan.Tests.DomainTests
{
    public class PropertyTest
    {
        [Fact]
        public void PropertiesWithTheSameValueAndAddress_AreEqual()
        {
            var propOne = new Property
            (
                new MonetaryAmount(100000),
                new Address
                    (
                    "PL",
                    "01-001",
                    "Warsaw",
                    "Zielona 7"
                    )
            );
            var propTwo = new Property
            (
                new MonetaryAmount(100000),
                new Address
                (
                    "PL",
                    "01-001",
                    "Warsaw",
                    "Zielona 7"
                )
            );

            Assert.True(propOne.Equals(propTwo));
        }

        [Fact]
        public void PropertiesWithDifferentValueAndTheSameAddress_AreNotEqual()
        {
            var propOne = new Property
            (
                new MonetaryAmount(100000),
                new Address
                (
                    "PL",
                    "01-001",
                    "Warsaw",
                    "Zielona 7"
                )
            );
            var propTwo = new Property
            (
                new MonetaryAmount(100001),
                new Address
                (
                    "PL",
                    "01-001",
                    "Warsaw",
                    "Zielona 7"
                )
            );

            Assert.False(propOne.Equals(propTwo));
        }

        [Fact]
        public void PropertiesWithTheSameValueAndDifferentAddress_AreNotEqual()
        {
            var propOne = new Property
            (
                new MonetaryAmount(100000),
                new Address
                (
                    "PL",
                    "01-001",
                    "Warsaw",
                    "Zielona 7"
                )
            );
            var propTwo = new Property
            (
                new MonetaryAmount(100000),
                new Address
                (
                    "PL",
                    "01-001",
                    "Warsaw",
                    "Zielona 8"
                )
            );

            Assert.False(propOne.Equals(propTwo));
        }
    }
}

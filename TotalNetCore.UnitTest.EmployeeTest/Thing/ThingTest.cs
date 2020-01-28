using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.UnitTest.EmployeeApp.Thing;
using Xunit;

namespace TotalNetCore.UnitTest.EmployeeTest.Thing
{
    public class ThingTest
    {
        private readonly Mock<IThingDependency> _moq;

        public ThingTest()
        {
            _moq = new Mock<IThingDependency>();
        }

        [Fact]
        public void X_DifferentInput_ReturnFullName()
        {
            //Arrange
            _moq.Setup(t => t.JoinUpper(It.IsAny<string>(), It.IsAny<string>()))
                .Returns("A B");

            _moq.Setup(t => t.Meaning)
                .Returns(42);

            //Act
            var thingTested = new ThingBeingTested(_moq.Object);
            var result = thingTested.X();

            //Assert
            Assert.Equal("A B = 42", result);
        }
    }
}

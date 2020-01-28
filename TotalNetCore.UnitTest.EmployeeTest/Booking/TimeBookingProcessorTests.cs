using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.UnitTest.Service.Booking;
using Xunit;

namespace TotalNetCore.UnitTest.EmployeeTest.Booking
{
    public class TimeBookingProcessorTests
    {

        private readonly  Mock<IBookingProcessor> _mock;
        private TimeBookingProcessor _timeBookingProcessor;

        public TimeBookingProcessorTests()
        {
            _mock = new Mock<IBookingProcessor>();
            _timeBookingProcessor = new TimeBookingProcessor(_mock.Object);

        }

        /// <summary>
        /// 测试参数不符合要求
        /// </summary>
        [Fact]
        public void BookTime_InvalidEmployeeId_ThrowArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(()=> _timeBookingProcessor.BookTime(new Employee { Id=-1,Name=""}, DateTime.Today, 8));
        }

        /// <summary>
        /// 测试参数不符合要求
        /// </summary>
        [Fact]
        public void BookTime_InvalidDate_ThrowArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _timeBookingProcessor.BookTime(new Employee { Id = 1, Name = "" }, DateTime.Now.AddDays(1), 8));
        }

        /// <summary>
        /// 测试参数不符合要求
        /// </summary>
        [Fact]
        public void BookTime_InvalidDuration_ThrowArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _timeBookingProcessor.BookTime(new Employee { Id = 1, Name = "" }, DateTime.Today, 12));
        }

        /// <summary>
        /// 测试参数符合要求
        /// </summary>
        [Fact]
        public void BookTime_ValidParameter_ReturnTrue()
        {
            Assert.True(_timeBookingProcessor.BookTime(new Employee { Id = 1, Name = "" }, DateTime.Today, 2));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.UnitTest.Service.Booking
{
    public class TimeBookingProcessor : ITimeBookingProcessor
    {
        private readonly IBookingProcessor bookingProcessor;

        public TimeBookingProcessor(IBookingProcessor bookingProcessor)
        {
            this.bookingProcessor = bookingProcessor;
        }
        public bool BookTime(Employee employee, DateTime date, decimal duration)
        {
            if(employee.Id <=0)
            {
                throw new ArgumentOutOfRangeException("Employee ID cannot be less than 0");
            }

            if(date.Date > DateTime.Today)
            {
                throw new ArgumentOutOfRangeException("Booking date cannot be greater than today");
            }

            if(duration > 9)
            {
                throw new ArgumentOutOfRangeException("You are working too hard, lets talk!");
            }

            return true;
        }
    }
}

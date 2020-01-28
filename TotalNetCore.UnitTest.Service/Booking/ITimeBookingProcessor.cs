using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.UnitTest.Service.Booking
{
    public interface ITimeBookingProcessor
    {
        bool BookTime(Employee employee, DateTime date, decimal duration);
    }
}

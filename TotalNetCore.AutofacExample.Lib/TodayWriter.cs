using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.AutofacExample.Lib
{
    public class TodayWriter : IDateWriter
    {
        private IOutput _output;

        public TodayWriter(IOutput output)
        {
            _output = output;
        }
        public void WriteDate()
        {
            _output.Write(DateTime.Today.ToShortDateString());
        }
    }
}

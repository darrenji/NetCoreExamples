using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Marketplace.Domain
{
    public class CurrencyMismatchException : Exception
    {

        public CurrencyMismatchException(string message) : base(message)
        {

        }
    }
}

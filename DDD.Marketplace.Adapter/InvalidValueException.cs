using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Marketplace.Adapter
{
    public class InvalidValueException : Exception
    {
        public InvalidValueException(Type type, string message) : base($"Value of {type.Name} {message}")
        {

        }
    }
}

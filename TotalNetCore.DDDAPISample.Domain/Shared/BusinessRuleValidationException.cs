using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.DDDAPISample.Domain.Shared
{
    public class BusinessRuleValidationException : Exception
    {
        public string Details { get; }

        public BusinessRuleValidationException(string message) : base(message)
        {

        }

        public BusinessRuleValidationException(string message, string details):base(message)
        {
            this.Details = details;
        }
    }
}

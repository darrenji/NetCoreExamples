using DDD.Marketplace.Adapter;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Marketplace.Domain.UserProfile
{
    public class FullName : Value<FullName>
    {
        public string Value { get; private set; }
        internal FullName(string value) => Value = value;

        public static FullName FromString(string fullName)
        {
            if (fullName.IsEmpty()) throw new ArgumentNullException(nameof(fullName));
            return new FullName(fullName);
        }

        protected FullName() { }

        public static implicit operator string(FullName fullName) => fullName.Value;
    }
}

using DDD.Marketplace.Adapter;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Marketplace.Domain
{
    public class UserId : Value<UserId>
    {
       private Guid Value { get; set; }

        public UserId(Guid value)
        {
            if(value==default)
            {
                throw new ArgumentNullException(nameof(value), "User id cannot be empty");
                Value = value;
            }
        }

        public static implicit operator Guid(UserId self) => self.Value;
    }
}

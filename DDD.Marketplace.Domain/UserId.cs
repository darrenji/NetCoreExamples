using DDD.Marketplace.Adapter;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Marketplace.Domain
{
    public class UserId : Value<UserId>
    {
        private readonly Guid _value;

        public UserId(Guid value)
        {
            _value = value;
        }
    }
}

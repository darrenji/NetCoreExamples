using DDD.Marketplace.Adapter;
using DDD.Marketplace.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Marketplace.Domain.UserProfile
{
    public class DisplayName : ValueTuple<DisplayName>
    {
        public string Value { get; private set; }

        internal DisplayName(string displayName) => Value = displayName;
        protected DisplayName() { }//序列化用

        public static DisplayName FromString(string displayName, CheckTextForProfanity hasProfanity)
        {
            if(displayName.IsEmpty())
            {
                throw new ArgumentNullException(nameof(displayName));
            }

            if(hasProfanity(displayName))
            {
                throw new DomainExceptions.ProfanityFound(displayName);
            }

            return new DisplayName(displayName);
        }

        public static implicit operator string(DisplayName displayName)
            => displayName.Value;
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Marketplace.Domain.Shared
{
   public static class DomainExceptions
    {
        public class InvalidEntityState : Exception
        {
            public InvalidEntityState(object entity, string message) : base($"Entity {entity.GetType().Name} state change refected, {message}")
            {

            }
        }

        public class ProfanityFound:Exception
        {

            public ProfanityFound(string text) : base($"Profanity found in text: {text}")
            {

            }

        }
    }
}

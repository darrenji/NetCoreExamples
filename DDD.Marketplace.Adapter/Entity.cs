using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Marketplace.Adapter
{
    public abstract class Entity<TId> where TId : IEquatable<TId>
    {
        protected abstract void EnsureValidState();
    }
}

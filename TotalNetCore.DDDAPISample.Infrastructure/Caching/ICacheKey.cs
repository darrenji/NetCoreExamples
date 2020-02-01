using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.DDDAPISample.Infrastructure.Caching
{
    public interface ICacheKey<TItem>
    {
        string CacheKey { get; }
    }
}

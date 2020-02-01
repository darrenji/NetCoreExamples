using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.DDDAPISample.Infrastructure.Caching
{
    public interface ICacheStoreItem
    {
        string CacheKey { get; }
    }
}

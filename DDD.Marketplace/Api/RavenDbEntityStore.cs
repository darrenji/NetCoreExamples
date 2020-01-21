using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.Marketplace.Api
{
    public class RavenDbEntityStore : IEntityStore
    {
        public Task<T> Load<T>(string id)
        {
            throw new NotImplementedException();
        }

        public Task Save<T>(T entity)
        {
            throw new NotImplementedException();
        }
    }
}

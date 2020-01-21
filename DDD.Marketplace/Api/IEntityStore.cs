using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.Marketplace.Api
{
    public interface IEntityStore
    {
        Task<T> Load<T>(string id);
        Task Save<T>(T entity);
    }
}

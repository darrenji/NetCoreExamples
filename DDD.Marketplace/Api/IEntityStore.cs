using DDD.Marketplace.Adapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.Marketplace.Api
{
    public interface IEntityStore
    {
        Task<T> Load<T>(string entityId) where T : Entity;
        Task Save<T>(T entity) where T : Entity;
        Task<bool> Exists<T>(string entityId);
    }
}

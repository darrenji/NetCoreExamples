using DDD.Marketplace.Domain;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.Marketplace.Infrastructure
{
    public class ClassifeidAdRepository : IClassifiedAdRepository
    {
        private readonly IAsyncDocumentSession _session;

        public ClassifeidAdRepository(IAsyncDocumentSession session)
        {
            _session = session;
        }
        public Task Add(ClassifiedAd entity)
        {
            return _session.StoreAsync(entity, EntityId(entity.Id));
        }

        public Task<bool> Exists(ClassifiedAddId id)
        {
            return _session.Advanced.ExistsAsync(EntityId(id));
        }

        public Task<ClassifiedAd> Load(ClassifiedAddId id)
        {
            return _session.LoadAsync<ClassifiedAd>(EntityId(id));
        }

        private static string EntityId(ClassifiedAddId id) => $"ClassifiedAd/{id.ToString()}";
    }
}

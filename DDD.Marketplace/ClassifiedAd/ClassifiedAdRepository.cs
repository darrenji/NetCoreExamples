using DDD.Marketplace.Domain;
using DDD.Marketplace.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.Marketplace.ClassifiedAd
{
    public class ClassifiedAdRepository : IClassifiedAdRepository, IDisposable
    {
        private readonly MarketplaceDbContext _db;
        public ClassifiedAdRepository(MarketplaceDbContext db)
        {
            _db = db;
        }
        public async Task Add(Domain.ClassifiedAd entity)
        {
            await _db.ClassifiedAds.AddAsync(entity);
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task<bool> Exists(ClassifiedAddId id)
        {
            return await _db.ClassifiedAds.FindAsync(id.Value) != null;
        }



        async Task<Domain.ClassifiedAd> IClassifiedAdRepository.Load(ClassifiedAddId id)
        {
            return await _db.ClassifiedAds.FindAsync(id.Value);
        }
    }
}

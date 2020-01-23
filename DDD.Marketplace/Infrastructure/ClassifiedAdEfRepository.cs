using DDD.Marketplace.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.Marketplace.Infrastructure
{
    public class ClassifiedAdEfRepository : IClassifiedAdRepository, IDisposable
    {
        private readonly ClassifiedAdDbContext _dbContext;

        public ClassifiedAdEfRepository(ClassifiedAdDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Add(ClassifiedAd entity)
        {
            await _dbContext.ClassifeidAds.AddAsync(entity);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task<bool> Exists(ClassifiedAddId id)
        {
            return (await _dbContext.ClassifeidAds.FindAsync(id.Value)) != null;
        }

        public async Task<ClassifiedAd> Load(ClassifiedAddId id)
        {
            return await _dbContext.ClassifeidAds.FindAsync(id.Value);
        }
    }
}

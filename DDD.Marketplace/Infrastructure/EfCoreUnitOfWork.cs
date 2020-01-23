using DDD.Marketplace.Adapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.Marketplace.Infrastructure
{
    public class EfCoreUnitOfWork : IUnitOfWork
    {
        private readonly MarketplaceDbContext _db;

        public EfCoreUnitOfWork(MarketplaceDbContext db)
        {
            _db = db;
        }
        public async Task Commit()
        {
            await _db.SaveChangesAsync();

        }
    }
}

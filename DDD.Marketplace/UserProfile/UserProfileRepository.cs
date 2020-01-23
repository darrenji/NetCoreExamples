using DDD.Marketplace.Domain;
using DDD.Marketplace.Domain.UserProfile;
using DDD.Marketplace.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.Marketplace.UserProfile
{
    public class UserProfileRepository : IUserProfileRepository, IDisposable
    {
        private readonly MarketplaceDbContext _dbContext;

        public UserProfileRepository(MarketplaceDbContext dbContext)
            => _dbContext = dbContext;

        public async Task Add(Domain.UserProfile.UserProfile entity)
            => await _dbContext.UserProfiles.AddAsync(entity);

        public async Task<bool> Exists(UserId id)
            => await _dbContext.UserProfiles.FindAsync(id.Value) != null;

        public async Task<Domain.UserProfile.UserProfile> Load(UserId id)
            =>await _dbContext.UserProfiles.FindAsync(id.Value);

        public void Dispose() => _dbContext.Dispose();
    }
}

using DDD.Marketplace.Domain;
using DDD.Marketplace.Domain.UserProfile;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.Marketplace.Infrastructure
{
    public class MarketplaceDbContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;

        public MarketplaceDbContext(DbContextOptions<MarketplaceDbContext> options, ILoggerFactory loggerFactory) : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        public DbSet<Domain.ClassifiedAd> ClassifiedAds { get; set; }
        public DbSet<Domain.UserProfile.UserProfile> UserProfiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory);
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClassifiedAdEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PictureEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserProfileEntityTypeConfiguration());
        }
    }

    public class ClassifiedAdEntityTypeConfiguration : IEntityTypeConfiguration<Domain.ClassifiedAd>
    {
        public void Configure(EntityTypeBuilder<Domain.ClassifiedAd> builder)
        {
            builder.HasKey(t => t.ClassifiedAdId);
            builder.OwnsOne(x => x.Id);
            builder.OwnsOne(x => x.Price, p=> p.OwnsOne(c=>c.Currency));
            builder.OwnsOne(x => x.Text);
            builder.OwnsOne(x => x.Title);
            builder.OwnsOne(x => x.ApprovedBy);
            builder.OwnsOne(x => x.OwnerId);
        }
    }

    public class PictureEntityTypeConfiguration : IEntityTypeConfiguration<Picture>
    {
        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder.HasKey(t => t.PictureId);
            builder.OwnsOne(x => x.Id);
            builder.OwnsOne(x => x.ParentId);
            builder.OwnsOne(x => x.Size);
        }
    }

    public class UserProfileEntityTypeConfiguration : IEntityTypeConfiguration<Domain.UserProfile.UserProfile>
    {
        public void Configure(EntityTypeBuilder<Domain.UserProfile.UserProfile> builder)
        {
            builder.HasKey(x => x.UserProfileId);
            builder.OwnsOne(x => x.DisplayName);
            builder.OwnsOne(x => x.Id);
            builder.OwnsOne(x => x.FullName);
        }
    }
}

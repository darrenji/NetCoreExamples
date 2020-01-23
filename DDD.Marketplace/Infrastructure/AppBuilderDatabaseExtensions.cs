using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.Marketplace.Infrastructure
{
    public static class AppBuilderDatabaseExtensions
    {
        public static IApplicationBuilder EnsureDatabase(this IApplicationBuilder builder)
        {
            //EnsureContextIsMigrated(builder.ApplicationServices.GetService<MarketplaceDbContext>());
            return builder;
        }

        private static void EnsureContextIsMigrated(DbContext context)
        {
            if(!context.Database.EnsureCreated())
            {
                
            }
        }
    }
}

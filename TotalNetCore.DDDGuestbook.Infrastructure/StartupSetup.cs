using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.DDDGuestbook.Infrastructure.Data;

namespace TotalNetCore.DDDGuestbook.Infrastructure
{
    public static class StartupSetup
    {
        public static void AddDbContext(this IServiceCollection services) =>
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer("Server=(localdb)\\v11.0;Database=cleanarchitecture;Trusted_Connection=True;MultipleActiveResultSets=true")); // will be created in web project root
    }
}

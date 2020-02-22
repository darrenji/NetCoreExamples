using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TotalNetCore.DDDLoan.Web.DomainModel;

namespace TotalNetCore.DDDLoan.Web.Persistence
{
    public class EfDbInitializer : IHostedService
    {
        private readonly IServiceProvider serviceProvider;

        public EfDbInitializer(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using(var scope  = serviceProvider.CreateScope())
            {
                var dbCtx = scope.ServiceProvider.GetService<LoanDbContext>();
                if(!dbCtx.Operators.Any(o=>o.Login=="admin"))
                {
                    var newOperatory = new Operator("admin", "admin", "admin", "admin", new MonetaryAmount(1_000_000M));
                    await dbCtx.SaveChangesAsync(cancellationToken);
                }
                
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}

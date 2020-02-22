using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotalNetCore.DDDLoan.Web.ReadModel
{
    public static class ReadModelInstaller
    {
        public static void AddReadModelServices(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton(_ => new LoanApplicationFinder(connectionString));
        }
    }
}

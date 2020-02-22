using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotalNetCore.DDDLoan.Web.DomainModel;

namespace TotalNetCore.DDDLoan.Web.Infrastructure.ExternalServices
{
    public static class ExtenalServiceInstaller
    {
        public static void AddExternalServiceClient(this IServiceCollection services)
        {
            services.AddSingleton<IDebtorRegistry, DebtorRegistry>();
        }
    }
}

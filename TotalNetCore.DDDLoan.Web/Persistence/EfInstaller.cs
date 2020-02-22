using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotalNetCore.DDDLoan.Web.DomainModel;
using TotalNetCore.DDDLoan.Web.DomainModel.Ddd;
using TotalNetCore.DDDLoan.Web.Persistence.Repositories;

namespace TotalNetCore.DDDLoan.Web.Persistence
{
    public static class EfInstaller
    {
        public static void AddEfAdapters(this IServiceCollection services, string connString)
        {
            //services.AddDbContext<LoanDbContext>(options => { options.UseMysql(connString)});

            services.AddDbContext<LoanDbContext>();
            services.AddScoped<IUnitOfWork, EfUnitOfWork>();
            services.AddScoped<IOperatorRepository, EfOperatorRepository>();
            services.AddHostedService<EfDbInitializer>();
        }
    }
}

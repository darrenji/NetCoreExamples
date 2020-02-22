using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotalNetCore.DDDLoan.Web.Application
{
    public static class ApplicationServicesInstaller
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<LoanApplicationSubmissionService>();
            services.AddScoped<LoanApplicationEvaluationService>();
            services.AddScoped<LoanApplicationDecisionService>();
        }
    }
}

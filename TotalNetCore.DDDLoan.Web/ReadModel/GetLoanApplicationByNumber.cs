using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TotalNetCore.DDDLoan.Web.Application.Dtos;

namespace TotalNetCore.DDDLoan.Web.ReadModel
{
    public static class GetLoanApplicationByNumber
    {
        public class Query : IRequest<LoanApplicationDto>
        {
            public string ApplicationNumber { get; set; }
        }

        public class Handler : IRequestHandler<Query, LoanApplicationDto>
        {
            private readonly string connectionString;

            public Handler(IConfiguration configuration)
            {
                this.connectionString = configuration.GetConnectionString("");
            }
            public Task<LoanApplicationDto> Handle(Query request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }

            private string BuildSelectDetailsQuery()
            {
                return string.Empty;
            }
        }
    }
}

using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TotalNetCore.DDDLoan.Web.Application.Dtos;

namespace TotalNetCore.DDDLoan.Web.ReadModel
{
    public static class FindLoanApplications
    {
        public class Query : IRequest<IEnumerable<LoanApplicationInfoDto>>
        {
            public LoanApplicationSearchCriteriaDto Criteria { get; set; }
        }

        public class Handler : IRequestHandler<Query, IEnumerable<LoanApplicationInfoDto>>
        {
            private readonly string connectionString;

            public Handler(IConfiguration configuration)
            {
                this.connectionString = configuration.GetConnectionString("");

            }
            public Task<IEnumerable<LoanApplicationInfoDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }

            private string BuildSearchQuery(LoanApplicationSearchCriteriaDto criteria)
            {
                var query = new StringBuilder();
                query.AppendLine("SELECT ");
                query.AppendLine("number AS Number, ");
                query.AppendLine("status AS Status, ");
                query.AppendLine("CustomerFirstName || ' ' || CustomerLastName AS CustomerName, ");
                query.AppendLine("decisionDate AS DecisionDate, ");
                query.AppendLine("LoanAmount AS LoanAmount, ");
                query.AppendLine("DecisionBy AS DecisionBy");
                query.AppendLine("FROM ddd_loan.loan_details_view");
                query.AppendLine("WHERE 1=1 ");

                if (!string.IsNullOrWhiteSpace(criteria.ApplicationNumber))
                {
                    query.AppendLine(" AND number = :ApplicationNumber");
                }

                if (!string.IsNullOrWhiteSpace(criteria.CustomerNationalIdentifier))
                {
                    query.AppendLine(" AND customerNationalIdentifier = :CustomerNationalIdentifier");
                }

                if (!string.IsNullOrWhiteSpace(criteria.DecisionBy))
                {
                    query.AppendLine(" AND decisionBy = :DecisionBy");
                }

                if (!string.IsNullOrWhiteSpace(criteria.RegisteredBy))
                {
                    query.AppendLine(" AND registeredBy = :RegisteredBy");
                }

                return query.ToString();
            }
        }
    }
}

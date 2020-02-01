﻿using Dapper;
using MediatR;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TotalNetCore.DDDAPISample.Application.Configuration.Data;
using TotalNetCore.DDDAPISample.Application.Payments;

namespace TotalNetCore.DDDAPISample.Infrastructure.Processing.Outbox
{
    [DisallowConcurrentExecution]
    public class ProcessOutboxJob : IJob
    {
        private readonly IMediator _mediator;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public ProcessOutboxJob(
            IMediator mediator,
            ISqlConnectionFactory sqlConnectionFactory)
        {
            _mediator = mediator;
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var connection = this._sqlConnectionFactory.GetOpenConnection();
            const string sql = "SELECT " +
                               "[OutboxMessage].[Id], " +
                               "[OutboxMessage].[Type], " +
                               "[OutboxMessage].[Data] " +
                               "FROM [app].[OutboxMessages] AS [OutboxMessage] " +
                               "WHERE [OutboxMessage].[ProcessedDate] IS NULL";

            var messages = await connection.QueryAsync<OutboxMessageDto>(sql);

            const string sqlUpdateProcessedDate = "UPDATE [app].[OutboxMessages] " +
                                                  "SET [ProcessedDate] = @Date " +
                                                  "WHERE [Id] = @Id";

            foreach (var message in messages)
            {
                Type type = Assembly.GetAssembly(typeof(PaymentCreatedNotification)).GetType(message.Type);
                var request = JsonConvert.DeserializeObject(message.Data, type);

                await this._mediator.Publish((INotification)request);

                await connection.ExecuteAsync(sqlUpdateProcessedDate, new
                {
                    Date = DateTime.UtcNow,
                    message.Id
                });
            }
        }
    }
}

using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotalNetCore.DDDLoan.Web.DomainModel.Ddd;

namespace TotalNetCore.DDDLoan.Web.Infrastructure.MessageQueue
{
    public static class MessageQueueClientInstaller
    {
        public static void AddRabbitMqClient(this IServiceCollection services, string brokerAddress)
        {
            services.AddSingleton<IBus>(_ => RabbitHutch.CreateBus(brokerAddress));
            services.AddSingleton<IEventPublisher, RabbitMqEventPublisher>();
        }
    }
}

﻿using DotNetCore.CAP;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotalNetCore.Micro.API.Application.IntegrationEvents
{
    public class SubscriberService : ISubscriberService, ICapSubscribe
    {
        IMediator _mediator;
        public SubscriberService(IMediator mediator)
        {
            _mediator = mediator;
        }

        [CapSubscribe("OrderPaymentSucceeded")]
        public void OrderPaymentSucceeded(OrderPaymentSucceededIntegrationEvent @event)
        {
            //Do SomeThing
        }

        [CapSubscribe("OrderCreated")]
        public void OrderCreated(OrderCreatedIntegrationEvent @event)
        {




            //Do SomeThing
        }
    }
}

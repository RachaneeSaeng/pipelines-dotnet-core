using MediatR;
using System;
using System.Collections.Generic;
using MediatR;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventServiceBus.Abstractions;
using EventServiceBus.Events;
using dotnetcoresample.IntegrationEvents.Events;

namespace dotnetcoresample.Customers
{
    public abstract class BaseRedisCommandHandler<TRequest>
        : BaseRedisRequestHandler<TRequest, Unit>
        where TRequest : IRequest<Unit>
    {
        private readonly IEventBus _eventBus;

        public BaseRedisCommandHandler(DotNetSampleDbContext context, ConnectionMultiplexer redis, IEventBus eventBus) : base(context, redis)
        {
            _eventBus = eventBus;
        }

        public override Task<Unit> Handle(TRequest request, CancellationToken cancellationToken)
        {
            return UpdateDbAndPublish(request, cancellationToken);
        }
        
        protected abstract Task<Unit> UpdateDbAndPublish(TRequest request, CancellationToken cancellationToken);

        protected void PublishEvent (string key, string oldValue, string newValue)
        {
            var eventMessage = new CacheValueChangedIntegrationEvent(key, oldValue, newValue);

            // Once basket is checkout, sends an integration event to
            // ordering.api to convert basket to order and proceeds with
            // order creation process
            _eventBus.Publish(eventMessage);
        }
    }
}

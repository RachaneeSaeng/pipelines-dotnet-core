using dotnetcoresample.IntegrationEvents.Events;
using EventServiceBus.Abstractions;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetcoresample.IntegrationEvents.EventHandling
{
    public class CacheValueChangedIntegrationEventHandler : IIntegrationEventHandler<CacheValueChangedIntegrationEvent>
    {
        private readonly ConnectionMultiplexer _redis;
        protected readonly IDatabase _redisdb;

        public CacheValueChangedIntegrationEventHandler(ConnectionMultiplexer redis)
        {
            _redis = redis;
            _redisdb = redis.GetDatabase();
        }

        public async Task Handle(CacheValueChangedIntegrationEvent @event)
        {
            await _redisdb.StringSetAsync(@event.Key, JsonConvert.SerializeObject(@event.NewValue));
        }

    }
}

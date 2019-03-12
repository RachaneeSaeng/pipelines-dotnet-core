using dotnetcoresample.IntegrationEvents.Events;
using EventServiceBus.Abstractions;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<CacheValueChangedIntegrationEventHandler> _logger;

        public CacheValueChangedIntegrationEventHandler(ConnectionMultiplexer redis, ILogger<CacheValueChangedIntegrationEventHandler> logger)
        {
            _redis = redis;
            _redisdb = redis.GetDatabase();
            _logger = logger;
        }

        public async Task Handle(CacheValueChangedIntegrationEvent @event)
        {
            await _redisdb.StringSetAsync(@event.Key, JsonConvert.SerializeObject(@event.NewValue));
            _logger.LogInformation($"Retrieved messaged from {Environment.MachineName}");
        }

    }
}

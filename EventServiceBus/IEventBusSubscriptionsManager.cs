using EventServiceBus.Abstractions;
using EventServiceBus.Events;
using System;
using System.Collections.Generic;

namespace EventServiceBus
{
    public interface IEventBusSubscriptionsManager
    {
        bool IsEmpty { get; }

        event EventHandler<string> OnEventRemoved;

        void AddSubscription<T, TH>()
           where T : IntegrationEvent
           where TH : IIntegrationEventHandler<T>;

        void AddDynamicSubscription<TH>(string eventName)
           where TH : IDynamicIntegrationEventHandler;

        void RemoveSubscription<T, TH>()
             where TH : IIntegrationEventHandler<T>
             where T : IntegrationEvent;

        void RemoveDynamicSubscription<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler;
        
        bool HasSubscriptionsForEvent<T>() where T : IntegrationEvent;
        bool HasSubscriptionsForEvent(string eventName);

        IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : IntegrationEvent;
        IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName);
        
        Type GetEventTypeByName(string eventName);

        //string GetEventKey<T>();

        void Clear();

    }
}
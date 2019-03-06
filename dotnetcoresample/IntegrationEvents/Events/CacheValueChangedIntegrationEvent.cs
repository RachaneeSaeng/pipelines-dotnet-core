using EventServiceBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetcoresample.IntegrationEvents.Events
{
    // Integration Events notes: 
    // An Event is “something that has happened in the past”, therefore its name has to be   
    // An Integration Event is an event that can cause side effects to other microsrvices, Bounded-Contexts or external systems.
    public class CacheValueChangedIntegrationEvent : IntegrationEvent
    {
        public string Key { get; private set; }

        public string OldValue { get; private set; }

        public string NewValue { get; private set; }

        public CacheValueChangedIntegrationEvent(string key, string oldValue, string newValue)
        {
            Key = key;
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}

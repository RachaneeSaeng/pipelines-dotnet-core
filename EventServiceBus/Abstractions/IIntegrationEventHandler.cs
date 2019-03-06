using EventServiceBus.Events;
using System.Threading.Tasks;

namespace EventServiceBus.Abstractions
{
    public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler 
        where TIntegrationEvent: IntegrationEvent
    {
        Task Handle(TIntegrationEvent @event); // strong event
    }

    public interface IIntegrationEventHandler
    {
    }
}

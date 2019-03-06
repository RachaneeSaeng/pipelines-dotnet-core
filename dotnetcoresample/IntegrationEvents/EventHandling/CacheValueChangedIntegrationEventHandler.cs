using dotnetcoresample.IntegrationEvents.Events;
using EventServiceBus.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetcoresample.IntegrationEvents.EventHandling
{
    public class CacheValueChangedIntegrationEventHandler : IIntegrationEventHandler<CacheValueChangedIntegrationEvent>
    {
        ///private readonly IBasketRepository _repository;

        public CacheValueChangedIntegrationEventHandler()
        {
            //_repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task Handle(CacheValueChangedIntegrationEvent @event)
        {
            //var userIds = _repository.GetUsers();

            //foreach (var id in userIds)
            //{
            //    var basket = await _repository.GetBasketAsync(id);

            //    await UpdatePriceInBasketItems(@event.ProductId, @event.NewPrice, @event.OldPrice, basket);
            //}
        }

        //private async Task UpdatePriceInBasketItems(int productId, decimal newPrice, decimal oldPrice, CustomerBasket basket)
        //{
        //    string match = productId.ToString();
        //    var itemsToUpdate = basket?.Items?.Where(x => x.ProductId == match).ToList();

        //    if (itemsToUpdate != null)
        //    {
        //        foreach (var item in itemsToUpdate)
        //        {
        //            if (item.UnitPrice == oldPrice)
        //            {
        //                var originalPrice = item.UnitPrice;
        //                item.UnitPrice = newPrice;
        //                item.OldUnitPrice = originalPrice;
        //            }
        //        }
        //        await _repository.UpdateBasketAsync(basket);
        //    }
        //}
    }
}

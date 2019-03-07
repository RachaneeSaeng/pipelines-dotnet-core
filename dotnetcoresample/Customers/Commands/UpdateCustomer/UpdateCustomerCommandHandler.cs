using dotnetcoresample.Models;
using EventServiceBus.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace dotnetcoresample.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : BaseRedisCommandHandler<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandHandler(DotNetSampleDbContext context, ConnectionMultiplexer redis, IEventBus eventBus) : base(context, redis, eventBus)
        {
        }

        protected override async Task<Unit> UpdateDbAndPublish(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Customers
                .SingleAsync(c => c.CustomerId == request.Id, cancellationToken);

            if (entity == null)
            {
                throw new Exception($"not found customer { request.Id }");
            }

            var oldValue = entity.CompanyName;

            entity.CompanyName = request.CompanyName;

            _context.Customers.Update(entity);

            await _context.SaveChangesAsync(cancellationToken);

            PublishEvent(request.Id, oldValue, request.CompanyName);

            return Unit.Value;
        }
    }
}

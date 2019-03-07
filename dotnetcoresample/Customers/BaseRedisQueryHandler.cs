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

namespace dotnetcoresample.Customers
{
    public abstract class BaseRedisQueryHandler<TRequest, TResponse>
        : BaseRedisRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {        
        public BaseRedisQueryHandler(DotNetSampleDbContext context, ConnectionMultiplexer redis) : base(context, redis)
        {
        }

        public override Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            return HandleWithFallback(request, cancellationToken);
        }

        private async Task<TResponse> HandleWithFallback(TRequest request, CancellationToken cancellationToken)
        {
            //fetch from cache
            var data = await GetFromCache(request, cancellationToken);
            // check if cache exist
            if (data != null)
                return data;

            return await GetFromDb(request, cancellationToken);
        }

        protected abstract Task<TResponse> GetFromCache(TRequest request, CancellationToken cancellationToken);
        protected abstract Task<TResponse> GetFromDb(TRequest request, CancellationToken cancellationToken);
    }
}

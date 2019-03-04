using MediatR;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace dotnetcoresample.Customers
{
    public abstract class BaseRedisRequestHandler<TRequest, TResponse> 
        : IRequestHandler<TRequest, TResponse>
        where TRequest: IRequest<TResponse>
    {
        protected readonly DotNetSampleDbContext _context;
        private readonly ConnectionMultiplexer _redis;
        protected readonly IDatabase _redisdb;

        public BaseRedisRequestHandler(DotNetSampleDbContext context, ConnectionMultiplexer redis)
        {
            _context = context;
            _redis = redis;
            _redisdb = redis.GetDatabase();
        }
        
        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}

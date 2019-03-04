using MediatR;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace dotnetcoresample.Customers.Queries.GetTopName
{
    public class GetTopNameQueryHandler : BaseRedisRequestHandler<GetTopNameQuery, string>
    {
        public GetTopNameQueryHandler(DotNetSampleDbContext context, ConnectionMultiplexer redis) 
            : base(context, redis)
        {
        }

        public override Task<string> Handle(GetTopNameQuery request, CancellationToken cancellationToken)
        {
            return GetTopName();
        }

        private async Task<string> GetTopName()
        {
            var data = await _redisdb.StringGetAsync("TopName");

            if (data.IsNullOrEmpty)
            {
                return null;
            }
            return data;
        }

    }
}

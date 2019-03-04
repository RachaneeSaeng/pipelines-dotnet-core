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
    public class GetTopNameQueryHandler : IRequestHandler<GetTopNameQuery, string>
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;

        public GetTopNameQueryHandler(ConnectionMultiplexer redis)
        {
            _redis = redis;
            _database = redis.GetDatabase();
        }

        public Task<string> Handle(GetTopNameQuery request, CancellationToken cancellationToken)
        {
            return GetTopName();
        }

        private async Task<string> GetTopName()
        {
            var data = await _database.StringGetAsync("TopName");

            if (data.IsNullOrEmpty)
            {
                return null;
            }
            return data;
        }

    }
}

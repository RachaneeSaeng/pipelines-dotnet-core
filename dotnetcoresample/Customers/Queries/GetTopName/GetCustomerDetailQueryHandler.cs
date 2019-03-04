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

        protected override async Task<string> GetFromCache(GetTopNameQuery request, CancellationToken cancellationToken)
        {
            var data = await _redisdb.StringGetAsync(request.Id);
            return data.IsNullOrEmpty ? null: JsonConvert.DeserializeObject<string>(data);
        }

        //Get data and also update cache
        protected override async Task<string> GetFromDb(GetTopNameQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Customers
                .FindAsync(request.Id);

            if (entity == null)
            {
                throw new Exception("Not found");
            }
            await _redisdb.StringSetAsync(request.Id, JsonConvert.SerializeObject(entity.CompanyName));

            return entity.CompanyName;
        }
    }
}

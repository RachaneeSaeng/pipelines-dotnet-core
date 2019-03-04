using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetcoresample.Models
{
    public class RedisRepository : IRedisRepository
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;

        public RedisRepository(ConnectionMultiplexer redis)
        {
            _redis = redis;
            _database = redis.GetDatabase();
        }

        public async Task<string> GetTopNameAsync(string id)
        {
            var data = await _database.StringGetAsync(id);

            if (data.IsNullOrEmpty)
            {
                return null;
            }
            return JsonConvert.DeserializeObject<string>(data);
        }
    }
}

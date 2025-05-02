using ScreenSearch.Domain.Interfaces.Caching;
using StackExchange.Redis;

namespace ScreenSearch.Infrastructure.Caching
{
    public class CacheStore(IConnectionMultiplexer connectionMultiplexer) : ICacheStore
    {
        private readonly IDatabase _database = connectionMultiplexer.GetDatabase();

        public async Task<string?> GetValueAsync(string key, TimeSpan? slidingExpiration = null)
        {
            RedisValue redisValue = await _database.StringGetAsync(key);

            if (!redisValue.IsNullOrEmpty && slidingExpiration.HasValue)
                await _database.KeyExpireAsync(key, slidingExpiration.Value);

            return redisValue.IsNullOrEmpty ? null : redisValue.ToString();
        }

        public async Task SetValueAsync(string key, string value, TimeSpan? expiry = null)
        {
            await _database.StringSetAsync(key, value, expiry);
        }

        public async Task SetValuesAsync(IDictionary<string, string> values, TimeSpan? expiry)
        {
            var batch = _database.CreateBatch();
            var tasks = new List<Task>();

            foreach (var value in values)
                tasks.Add(batch.StringSetAsync(value.Key, value.Value, expiry));

            batch.Execute();
            
            await Task.WhenAll(tasks);
        }
    }
}

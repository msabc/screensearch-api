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

        public async Task RemoveValueAsync(string key)
        {
            await _database.KeyDeleteAsync(key);
        }

        public async Task SetValueAsync(string key, string value, TimeSpan? expiry = null)
        {
            await _database.StringSetAsync(key, value, expiry);
        }
    }
}

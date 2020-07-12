using Microsoft.Extensions.Options;
using RIPE.CrossCutting.Options;
using System.Text.Json;

namespace RIPE.Data.Repositories.Cache
{
    public abstract class CacheRepository
    {
        private readonly IOptions<RedisOptions> _redisOptions;
        protected readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions { IgnoreReadOnlyProperties = true };

        public CacheRepository(IOptions<RedisOptions> redisOptions)
        {
            _redisOptions = redisOptions;
        }

        protected string GetCacheKey(string customerId) => string.Format(_redisOptions.Value.QuestionsCacheKey, customerId);
    }
}

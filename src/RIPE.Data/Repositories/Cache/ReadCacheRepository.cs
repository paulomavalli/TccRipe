using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RIPE.Application.Interfaces.Repository.Cache;
using RIPE.Application.Interfaces.Repository.Cache.DistributedCache;
using RIPE.CrossCutting.Options;
using RIPE.Domain.Domains;
using RIPE.Domain.Domains.Questions;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace RIPE.Data.Repositories.Cache
{
    public class ReadCacheRepository : CacheRepository, IReadCacheRepository
    {
        private readonly IDistributedCache _cache;
        private readonly string _key;
        private readonly ILogger<CacheRepository> _logger;

        public ReadCacheRepository(IReadDistributedCache cache, ILogger<CacheRepository> logger, IOptions<RedisOptions> redisOptions)
            : base(redisOptions)
        {
            _cache = cache;
            _logger = logger;
            _key = redisOptions.Value.CacheKey;
        }

        public async Task<IEnumerable<TypeQuestions>> GetQuestions()
        {
            try
            {
                _logger.LogInformation("Obtendo cache para o chave {ReasonsZendesk}", _key);
                var cacheValue = await _cache.GetStringAsync(GetCacheKey(_key));
                if (string.IsNullOrEmpty(cacheValue))
                    return null;

                var ListReasons = JsonSerializer.Deserialize<IEnumerable<TypeQuestions>>(cacheValue, jsonSerializerOptions);
                _logger.LogInformation("Cache obtido com sucesso", _key, ListReasons);

                return ListReasons;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Falha ao obter o cache para a chave {ReasonsZendesk}", _key);
                throw;
            }
        }

        public async Task<IEnumerable<BestHabits>> GetHabits()
        {
            try
            {
                _logger.LogInformation("Obtendo cache para o chave {ReasonsZendesk}", _key);
                var cacheValue = await _cache.GetStringAsync(GetCacheKey(_key));
                if (string.IsNullOrEmpty(cacheValue))
                    return null;

                var ListReasons = JsonSerializer.Deserialize<IEnumerable<BestHabits>>(cacheValue, jsonSerializerOptions);
                _logger.LogInformation("Cache obtido com sucesso", _key, ListReasons);

                return ListReasons;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Falha ao obter o cache para a chave {ReasonsZendesk}", _key);
                throw;
            }
        }

        public async Task<IEnumerable<UserDetails>> GetUser()
        {
            try
            {
                _logger.LogInformation("Obtendo cache para o chave {ReasonsZendesk}", _key);
                var cacheValue = await _cache.GetStringAsync(GetCacheKey(_key));
                if (string.IsNullOrEmpty(cacheValue))
                    return null;

                var ListReasons = JsonSerializer.Deserialize<IEnumerable<UserDetails>>(cacheValue, jsonSerializerOptions);
                _logger.LogInformation("Cache obtido com sucesso", _key, ListReasons);

                return ListReasons;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Falha ao obter o cache para a chave {ReasonsZendesk}", _key);
                throw;
            }
        }

        public async Task<IEnumerable<TypeQuestions>> GetResults()
        {
            try
            {
                _logger.LogInformation("Obtendo cache para o chave {ReasonsZendesk}", _key);
                var cacheValue = await _cache.GetStringAsync(GetCacheKey(_key));
                if (string.IsNullOrEmpty(cacheValue))
                    return null;

                var ListReasons = JsonSerializer.Deserialize<IEnumerable<TypeQuestions>>(cacheValue, jsonSerializerOptions);
                _logger.LogInformation("Cache obtido com sucesso", _key, ListReasons);

                return ListReasons;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Falha ao obter o cache para a chave {ReasonsZendesk}", _key);
                throw;
            }
        }
    }
}

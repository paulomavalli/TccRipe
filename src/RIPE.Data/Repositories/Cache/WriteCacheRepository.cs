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
    public class WriteCacheRepository : CacheRepository, IWriteCacheRepository
    {
        private readonly IDistributedCache _cache;
        private readonly ILogger<CacheRepository> _logger;
        private readonly string _key;

        public WriteCacheRepository(IWriteDistributedCache cache, ILogger<CacheRepository> logger, IOptions<RedisOptions> redisOptions)
            : base(redisOptions)
        {
            _cache = cache;
            _logger = logger;
            _key = redisOptions.Value.CacheKey;
        }

        public async Task SetResultQuestions(IEnumerable<TypeQuestions> policyResult)
        {
            try
            {
                _logger.LogInformation("Salvando cache para a lista de reasons", _key);
                await _cache.SetStringAsync(GetCacheKey(_key), JsonSerializer.Serialize(policyResult, base.jsonSerializerOptions));
                _logger.LogInformation("Cache salva com sucesso", _key);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Falha ao adicionar no cache a lista de reasons");
            }
        }

        public async Task SetLogin(UserDetails newUser)
        {
            try
            {
                _logger.LogInformation("Salvando cache para a lista de reasons", _key);
                await _cache.SetStringAsync(GetCacheKey(_key), JsonSerializer.Serialize(newUser, base.jsonSerializerOptions));
                _logger.LogInformation("Cache salva com sucesso", _key);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Falha ao adicionar no cache a lista de reasons");
            }
        }

        public async Task SetReport(IEnumerable<TypeQuestions> policyResult)
        {
            try
            {
                _logger.LogInformation("Salvando cache para a lista de reasons", _key);
                await _cache.SetStringAsync(GetCacheKey(_key), JsonSerializer.Serialize(policyResult, base.jsonSerializerOptions));
                _logger.LogInformation("Cache salva com sucesso", _key);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Falha ao adicionar no cache a lista de reasons");
            }
        }
    }
}

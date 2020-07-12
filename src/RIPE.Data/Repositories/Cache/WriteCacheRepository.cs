using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RIPE.Application.Interfaces.Repository.Cache;
using RIPE.Application.Interfaces.Repository.Cache.DistributedCache;
using RIPE.CrossCutting.Options;
using RIPE.Domain.Domains;
using RIPE.Domain.Domains.Feedback;
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
        private readonly string _userKey;
        private readonly string _feedbackKey;

        public WriteCacheRepository(IWriteDistributedCache cache, ILogger<CacheRepository> logger, IOptions<RedisOptions> redisOptions)
            : base(redisOptions)
        {
            _cache = cache;
            _logger = logger;
            _userKey = redisOptions.Value.UserCacheKey;
            _feedbackKey = redisOptions.Value.FeedbackCacheKey;
        }

        public async Task SetResultQuestions(IEnumerable<TypeQuestions> policyResult)
        {
            try
            {
                _logger.LogInformation("Salvando cache para a lista de reasons", _userKey);
                await _cache.SetStringAsync(GetCacheKey(_userKey), JsonSerializer.Serialize(policyResult, base.jsonSerializerOptions));
                _logger.LogInformation("Cache salva com sucesso", _userKey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Falha ao adicionar no cache a lista de reasons");
            }
        }

        public async Task SetLogin(IEnumerable<UserDetails> newUser)
        {
            try
            {
                _logger.LogInformation("Salvando cache para a lista de reasons", _userKey);
                await _cache.SetStringAsync(GetCacheKey(_userKey), JsonSerializer.Serialize(newUser, base.jsonSerializerOptions));
                _logger.LogInformation("Cache salva com sucesso", _userKey);
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
                _logger.LogInformation("Salvando cache para a lista de reasons", _userKey);
                await _cache.SetStringAsync(GetCacheKey(_userKey), JsonSerializer.Serialize(policyResult, base.jsonSerializerOptions));
                _logger.LogInformation("Cache salva com sucesso", _userKey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Falha ao adicionar no cache a lista de reasons");
            }
        }

        public async Task SetFeedBack(IEnumerable<Feedback> feedback)
        {
            try
            {
                _logger.LogInformation("Salvando cache para a lista de reasons", _feedbackKey);
                await _cache.SetStringAsync(GetCacheKey(_feedbackKey), JsonSerializer.Serialize(feedback, base.jsonSerializerOptions));
                _logger.LogInformation("Cache salva com sucesso", _feedbackKey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Falha ao adicionar no cache a lista de reasons");
            }
        }
    }
}

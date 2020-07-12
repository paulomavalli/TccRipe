using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Options;
using RIPE.Application.Interfaces.Repository.Cache.DistributedCache;
using RIPE.CrossCutting.Options;
using System.Diagnostics.CodeAnalysis;

namespace Easynvest.Loan.Application.Cache
{
    [ExcludeFromCodeCoverage]
    public class WriteDistributedCache : RedisCache, IWriteDistributedCache
    {
        public WriteDistributedCache(IOptions<WriteRedisOptions> optionsAccessor) : base(optionsAccessor)
        {
        }
    }
}

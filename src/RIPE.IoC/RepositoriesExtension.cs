using Microsoft.Extensions.DependencyInjection;
using RIPE.Application.Interfaces.Repository;
using RIPE.Application.Interfaces.Repository.Cache;
using RIPE.Data.Repositories;
using RIPE.Data.Repositories.Cache;

namespace RIPE.IoC
{
    public static class RepositoriesExtension
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddTransient<IRipeRepository, RipeRepository>();
            services.AddTransient<ICollateralPriorityRepository, FeedbackRepository>();
            services.AddTransient<IReadCacheRepository, ReadCacheRepository>();
            services.AddTransient<IWriteCacheRepository, WriteCacheRepository>();
            return services;
        }
    }
}

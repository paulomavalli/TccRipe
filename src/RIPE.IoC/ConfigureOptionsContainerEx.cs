using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RIPE.CrossCutting.Options;

namespace RIPE.IoC
{
    public static class ConfigureOptionsContainerEx
    {
        public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            var redisOptions = new RedisOptions();
            configuration.GetSection(nameof(RedisOptions)).Bind(redisOptions);
            services.Configure<ConnectionStringOptions>(configuration.GetSection("ConnectionStrings"));
            services.Configure<RedisOptions>(configuration.GetSection("RedisOptions"));
            services.Configure<WriteRedisOptions>(options => options.Configuration = redisOptions.ConnectionStringWrite);
            services.Configure<ReadRedisOptions>(options => options.Configuration = redisOptions.ConnectionStringRead);

            return services;
        }
    }
}
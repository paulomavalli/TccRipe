using RIPE.CrossCutting.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RIPE.IoC
{
    public static class ConfigureOptionsContainerEx
    {
        public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConnectionStringOptions>(configuration.GetSection("ConnectionStrings"));
            return services;
        }
    }
}
using RIPE.Application.Interfaces.Repository;
using RIPE.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace RIPE.IoC
{
    public static class RepositoriesExtension
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddTransient<IRipeRepository, RipeRepository>();
            services.AddTransient<ICollateralPriorityRepository, CollateralPriorityRepository>();
            return services;
        }
    }
}

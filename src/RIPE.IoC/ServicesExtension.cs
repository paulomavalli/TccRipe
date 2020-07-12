using Easynvest.Loan.Application.Cache;
using Microsoft.Extensions.DependencyInjection;
using RIPE.Application.Interfaces.Repository.Cache.DistributedCache;
using RIPE.Domain.Domains.PriorityAggregate;

namespace RIPE.IoC
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IReportService, ReportService>();
            services.Add(ServiceDescriptor.Singleton<IWriteDistributedCache, WriteDistributedCache>());
            services.Add(ServiceDescriptor.Singleton<IReadDistributedCache, ReadDistributedCache>());

            return services;
        }

    }
}

using RIPE.Domain.Domains.PriorityAggregate;
using Microsoft.Extensions.DependencyInjection;

namespace RIPE.IoC
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IReportService, ReportService>();

            return services;
        }

    }
}

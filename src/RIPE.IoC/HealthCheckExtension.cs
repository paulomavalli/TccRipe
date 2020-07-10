using RIPE.CrossCutting;
using RIPE.CrossCutting.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace RIPE.IoC
{
    public static class HealthCheckExtension
    {
        public static IServiceCollection AddHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionStringOptions = new ConnectionStringOptions();
            configuration.GetSection("ConnectionStrings").Bind(connectionStringOptions);

            services.AddHealthChecks()
                    .AddMySql(connectionStringOptions.MySQLDbConnection, "MySQL", HealthStatus.Unhealthy);

            return services;
        }

        public static IEndpointRouteBuilder UseHealthChecks(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapHealthChecks("/health", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = HealthCheckResponseWriter.WriteResponse
            });
            endpoints.MapGet("/ping", async context => await context.Response.WriteAsync("pong"));

            return endpoints;
        }
    }
}

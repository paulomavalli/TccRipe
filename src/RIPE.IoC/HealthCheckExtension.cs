using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RIPE.CrossCutting;
using RIPE.CrossCutting.Options;

namespace RIPE.IoC
{
    public static class HealthCheckExtension
    {
        public static IServiceCollection AddHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionStringOptions = new ConnectionStringOptions();
            configuration.GetSection("ConnectionStrings").Bind(connectionStringOptions);

            var redisOptions = new RedisOptions();
            configuration.GetSection(nameof(RedisOptions)).Bind(redisOptions);

            services.AddHealthChecks()
                .AddMySql(connectionStringOptions.MySQLDbConnection, "MySQL", HealthStatus.Unhealthy)
                .AddRedis(redisOptions.ConnectionStringWrite, name: "RedisWrite", failureStatus: HealthStatus.Degraded)
                .AddRedis(redisOptions.ConnectionStringRead, name: "RedisReadOnly", failureStatus: HealthStatus.Degraded);

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

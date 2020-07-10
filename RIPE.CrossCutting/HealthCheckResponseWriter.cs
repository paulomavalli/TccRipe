using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace RIPE.CrossCutting
{
    [ExcludeFromCodeCoverage]
    public static class HealthCheckResponseWriter
    {
        public static Task WriteResponse(HttpContext httpContext, HealthReport result)
        {
            var resultObj = new
            {
                status = result.Status.ToString(),
                healthy = result.Entries.Where(e => e.Value.Status == HealthStatus.Healthy).Select(s => new
                {
                    check = s.Key,
                    duration = s.Value.Duration.ToString(),
                    status = "OK",
                    endpoint = s.Value.Tags.FirstOrDefault()
                }),
                degraded = result.Entries.Where(e => e.Value.Status == HealthStatus.Degraded).Select(s => new
                {
                    check = s.Key,
                    duration = s.Value.Duration.ToString(),
                    exception = s.Value.Exception != null ? s.Value.Exception.Message : s.Value.Description,
                    endpoint = s.Value.Tags.FirstOrDefault()
                }),
                unhealthy = result.Entries.Where(x => x.Value.Status == HealthStatus.Unhealthy).Select(s => new
                {
                    check = s.Key,
                    duration = s.Value.Duration.ToString(),
                    exception = s.Value.Exception != null ? s.Value.Exception.Message : s.Value.Description,
                    endpoint = s.Value.Tags.FirstOrDefault()
                })
            };

            return httpContext.Response.WriteAsync(JsonSerializer.Serialize(resultObj,
                new JsonSerializerOptions {WriteIndented = true, IgnoreNullValues = true}));
        }
    }
}
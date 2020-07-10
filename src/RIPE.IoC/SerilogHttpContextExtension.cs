using System.Linq;
using Microsoft.AspNetCore.Http;
using Serilog.Enrichers.AspnetcoreHttpcontext;

namespace RIPE.IoC
{
    public static class SerilogHttpContextExtension
    {
        public static object CustomEnrichLogic(IHttpContextAccessor hca)
        {
            var ctx = hca.HttpContext;
            if (ctx == null) return null;

            var httpContextCache = new HttpContextCache
            {
                IpAddress = ctx.Connection.RemoteIpAddress.ToString(),
                Host = ctx.Request.Host.ToString(),
                Path = ctx.Request.Path.ToString(),
                IsHttps = ctx.Request.IsHttps,
                Scheme = ctx.Request.Scheme,
                Method = ctx.Request.Method,
                ContentType = ctx.Request.ContentType,
                Protocol = ctx.Request.Protocol,
                QueryString = ctx.Request.QueryString.ToString(),
                Query = ctx.Request.Query.ToDictionary(x => x.Key, y => y.Value.ToString()),
                Headers = ctx.Request.Headers.ToDictionary(x => x.Key, y => y.Value.ToString())
            };

            httpContextCache.Headers.Remove("Authorization");
            httpContextCache.Headers.Remove("Cookie");

            return httpContextCache;
        }
    }
}
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using RIPE.IoC;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Compression;
using System.Linq;

namespace RIPE.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddOptions(Configuration)
                .AddJwtAuthentication(Configuration)
                .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>()
                .AddSwaggerGen(c => c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()))
                .AddApiVersioning(options =>
                {
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.ReportApiVersions = true;
                })
                .AddHandlers()
                .RegisterRepositories()
                .AddServices()
                .AddPollyPolices(Configuration)
                .AddHealthCheck(Configuration)
                .AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.IgnoreNullValues = true);

            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);
            services.AddResponseCompression(options => { options.Providers.Add<GzipCompressionProvider>(); });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            var cultureInfo = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.UseHealthChecks();
            });

            if (!env.IsProduction())
                app
                    .UseSwagger(c =>
                    {
                        c.PreSerializeFilters.Add((apiDoc, httpReq) =>
                        {
                            apiDoc.Servers = new List<OpenApiServer>
                            {
                                new OpenApiServer {Url = $"{httpReq.Scheme}://{httpReq.Host.Value}", Description = "Development"},
                                new OpenApiServer {Url = $"{httpReq.Scheme}://{httpReq.Host.Value}/Collateral", Description = "Kubernetes"}
                            };
                        });
                    })
                    .UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "v1"));
        }
    }
}

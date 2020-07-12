using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RIPE.IoC;
using Serilog;
using Serilog.Enrichers.AspnetcoreHttpcontext;
using Serilog.Events;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.LogstashHttp;
using System;
using System.Reflection;

namespace RIPE.API
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application ended unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                        .UseSerilog(CreateLoggerConfiguration);
                });
        }

        private static void CreateLoggerConfiguration(IServiceProvider provider, WebHostBuilderContext hostingContext,
            LoggerConfiguration loggerConfiguration)
        {
            var assembly = Assembly.GetExecutingAssembly().GetName();

            loggerConfiguration
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Error)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Assembly", $"{assembly.Name}")
                .Enrich.WithProperty("Version", $"{assembly.Version}")
                .Enrich.WithProperty("Jornada", "credit-collateral")
                .Enrich.WithAspnetcoreHttpcontext(provider, SerilogHttpContextExtension.CustomEnrichLogic)
                .WriteTo.Console(
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}",
                    restrictedToMinimumLevel: LogEventLevel.Debug)
                .WriteTo.LogstashHttp(
                    new LogstashHttpSinkOptions()
                    {
                        LogstashUri = hostingContext.Configuration.GetConnectionString("Logstash"),
                        CustomFormatter =
                            new ElasticsearchJsonFormatter(inlineFields: true, renderMessageTemplate: false)
                    })
                .ReadFrom.Configuration(hostingContext.Configuration);
        }
    }
}

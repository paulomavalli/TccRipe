using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Timeout;
using RIPE.CrossCutting.Extensions;
using RIPE.CrossCutting.Options;
using System;
using System.Net.Http;

namespace RIPE.IoC
{
    public static class PolicesExtension
    {
        public static IServiceCollection AddPollyPolices(this IServiceCollection services, IConfiguration configuration)
        {
            var config = new CircuitBreakOptions();
            configuration.GetSection("HttpPolicySettings").Bind(config);

            var timeoutTryPolicy =
                Policy.TimeoutAsync(config.CircuitBreakerTimeoutInSeconds, TimeoutStrategy.Pessimistic);

            var circuitBreakerPolicy = Policy.Handle<Exception>()
                                             .CircuitBreakerAsync(
                                                 config.NumberOfExceptionsAllowedBeforeBreaking,
                                                 TimeSpan.FromSeconds(config.DurationOfBreakInSeconds),
                                                 OnBreak,
                                                 OnReset)
                                             .WrapAsync(timeoutTryPolicy);
            var waitAndRetryPolicy = Policy.Handle<Exception>()
                                           .WaitAndRetryAsync(
                                               config.NumberOfRetryAttempts,
                                               retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                                               OnRetry
                                           )
                                           .WrapAsync(circuitBreakerPolicy);

            Policy.TimeoutAsync(config.TotalResponseTimeoutInSeconds, TimeoutStrategy.Pessimistic)
                                   .WrapAsync(waitAndRetryPolicy).AsAsyncPolicy<HttpResponseMessage>();

            return services;
        }

        private static void OnBreak(Exception exception, TimeSpan timeSpan, Context context)
        {
            var logger = context.GetLogger();
            logger.LogWarning(exception, $"CircuitBreaker: o circuito está aberto à {timeSpan.TotalSeconds} segundos!",
                timeSpan.TotalSeconds);
        }

        private static void OnReset(Context context)
        {
            var logger = context.GetLogger();
            logger.LogWarning("CircuitBreaker: o circuito está fechado novamente!");
        }

        private static void OnRetry(Exception exception, TimeSpan timeSpan, int retries, Context context)
        {
            var logger = context.GetLogger();
            logger.LogWarning(exception, $"WaitAndRetry: Tentativa {retries}, Espera: {timeSpan.TotalSeconds} segundos",
                retries,
                timeSpan.TotalSeconds);
        }
    }
}

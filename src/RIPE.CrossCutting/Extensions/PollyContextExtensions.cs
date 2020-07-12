using Microsoft.Extensions.Logging;
using Polly;
using RIPE.CrossCutting.Policy;
using System.Diagnostics.CodeAnalysis;

namespace RIPE.CrossCutting.Extensions
{
    [ExcludeFromCodeCoverage]

    public static class PollyContextExtensions
    {
        public static Context WithLogger<T>(this Context context, ILogger<T> logger)
        {
            context[PolicyContextKeys.LOGGER_KEY] = logger;
            return context;
        }

        public static ILogger GetLogger(this Context context)
        {
            if (context.TryGetValue(PolicyContextKeys.LOGGER_KEY, out var logger)) return logger as ILogger;

            return null;
        }
    }
}
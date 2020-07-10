using System.Diagnostics.CodeAnalysis;

namespace RIPE.CrossCutting.Options
{
    [ExcludeFromCodeCoverage]
    public class CircuitBreakOptions
    {
        public int CircuitBreakerTimeoutInSeconds { get; set; }
        public int DurationOfBreakInSeconds { get; set; }
        public int NumberOfExceptionsAllowedBeforeBreaking { get; set; }
        public int TotalResponseTimeoutInSeconds { get; set; }
        public int NumberOfRetryAttempts { get; set; }
    }
}

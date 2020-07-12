using System.Diagnostics.CodeAnalysis;

namespace RIPE.CrossCutting.Options
{
    [ExcludeFromCodeCoverage]
    public class RedisOptions
    {
        public string ConnectionStringWrite { get; set; }
        public string ConnectionStringRead { get; set; }

        public string LoginCacheKey { get; set; }
        public string QuestionsCacheKey { get; set; }
        public string ReportCacheKey { get; set; }
        public string UserCacheKey { get; set; }
        public string FeedbackCacheKey { get; set; }
    }
}

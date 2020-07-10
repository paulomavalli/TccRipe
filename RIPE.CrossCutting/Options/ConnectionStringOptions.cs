using System.Diagnostics.CodeAnalysis;

namespace RIPE.CrossCutting.Options
{
    [ExcludeFromCodeCoverage]
    public class ConnectionStringOptions
    {
        public string MySQLDbConnection { get; set; }
    }
}
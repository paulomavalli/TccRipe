using Dapper;
using RIPE.Application.Interfaces.Repository;
using RIPE.CrossCutting.Options;
using RIPE.Data.Statements;
using RIPE.Domain.Domains;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RIPE.Data.Repositories
{
    public class FeedbackRepository : ICollateralPriorityRepository
    {
        private readonly ConnectionStringOptions _connectionStringOptions;
        private readonly ILogger _logger;

        public FeedbackRepository(ILogger<FeedbackRepository> logger, IOptions<ConnectionStringOptions> connectionStringOptions)
        {
            _logger = logger;
            _connectionStringOptions = connectionStringOptions.Value;
        }

        public async Task<IEnumerable<CollateralPriority>> GetCollateralPriority()
        {
            await using var conn = new MySqlConnection(_connectionStringOptions.MySQLDbConnection);
            _logger.LogDebug("Consultando Prioridade dos produtos.");
            return await conn.QueryAsync<CollateralPriority>(CollateralPriorityStatements.GET_COLLATERAL_PRIORITY);
        }

        public Task WriteNewUser(string login, string password)
        {
            throw new System.NotImplementedException();
        }
    }
}

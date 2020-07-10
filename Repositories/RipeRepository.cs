using Dapper;
using RIPE.Application.Interfaces.Repository;
using RIPE.CrossCutting.Options;
using RIPE.Data.Statements;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using RIPE.Domain.Domains.Feedback;
using RIPE.Domain.Domains.Questions;

namespace RIPE.Data.Repositories
{
    public class RipeRepository : IRipeRepository
    {
        private readonly ConnectionStringOptions _connectionStringOptions;
        private readonly ILogger<RipeRepository> _logger;

        public RipeRepository(ILogger<RipeRepository> logger, IOptions<ConnectionStringOptions> connectionStringOptions)
        {
            _logger = logger;
            _connectionStringOptions = connectionStringOptions.Value;
        }

        public async Task<IEnumerable<Domain.Domains.Collateral>> GetCollateral(string customerId)
        {
            await using var conn = new MySqlConnection(_connectionStringOptions.MySQLDbConnection);
            _logger.LogDebug("Consultando Ativos em Garantia do cliente.");
            return await conn.QueryAsync<Domain.Domains.Collateral>(RipeStatements.GET_COLLATERAL, new {customerId });
        }

        public async Task<IEnumerable<Domain.Domains.Collateral>> GetCollateralPerSecurityId(string securityId, string customerId)
        {
            await using var conn = new MySqlConnection(_connectionStringOptions.MySQLDbConnection);
            _logger.LogDebug("Consultando Ativos em Garantia do cliente.");
            return await conn.QueryAsync<Domain.Domains.Collateral>(RipeStatements.GET_COLLATERAL_PER_SECURITY_ID, new { securityId, customerId });
        }

        public async Task<IEnumerable<TypeQuestions>> GetQuestions()
        {
            await using var conn = new MySqlConnection(_connectionStringOptions.MySQLDbConnection);
            _logger.LogDebug("Consultando Ativos em Garantia do cliente.");
            return await conn.QueryAsync<TypeQuestions>(RipeStatements.GET_COLLATERAL);
        }

        public async Task<bool>WriteFeedback(Feedback feedback)
        {
            try
            {

                await using var conn = new MySqlConnection(_connectionStringOptions.MySQLDbConnection);
                _logger.LogDebug("Gravando feedback.");
                await conn.ExecuteAsync(RipeStatements.WRITE_FEEDBACK, feedback);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao armazenar feedback do usuário" +
                $" ");
                return false;
            }
        }

        public async Task<bool> WriteNewUser(string login, string password)
        {
            try
            {
                await using var conn = new MySqlConnection(_connectionStringOptions.MySQLDbConnection);
                _logger.LogDebug("Gravando propostas de parcelamento do cliente.");

                DateTime requestDate = DateTime.Today;
                await conn.ExecuteAsync(RipeStatements.WRITE_USER,new { login,password, requestDate } );
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Login: {login} - Erro ao armazenar este novo usuário" +
                $" ");
                return false;
            }
        }
    }
}

using System.Data;
using System.Data.SqlClient;
using Poker.Core.DAL;
using Poker.Core.Validation;

namespace Poker.Server.DataAccess
{
    public class MssqlConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public MssqlConnectionFactory()
        {
        }

        public MssqlConnectionFactory(string connectionString)
        {
            Contract.RequiresNotEmpty(connectionString, $"'{nameof(connectionString)}' is invalid");

            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}

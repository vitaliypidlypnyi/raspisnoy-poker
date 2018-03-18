using System.Data.SqlClient;

namespace Poker.Server.Database.Helpers
{
    public static class SqlHelper
    {
        public static string GetDbName(string connectionString)
        {
            var builder = new SqlConnectionStringBuilder(connectionString);
            string database = builder.InitialCatalog;
            return database;
        }
    }
}

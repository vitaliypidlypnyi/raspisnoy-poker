using System.Data;

namespace Poker.Core.DAL
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}

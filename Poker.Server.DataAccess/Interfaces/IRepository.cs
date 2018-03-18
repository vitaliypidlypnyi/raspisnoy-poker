using System.Collections.Generic;
using System.Threading.Tasks;
using Poker.Core.Maps;
using Poker.Core.SqlBuilders;
using Poker.Core.ColumnOperations;

namespace Poker.Server.DataAccess
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> SelectFirstAsync(TableMap tableMap, Filter filter);

        Task<TEntity> SelectFirstAsync(TableMap tableMap, SelectColumn columns, Filter filter);

        Task<IEnumerable<TEntity>> SelectAllAsync(TableMap tableMap, Filter filter);

        Task<IEnumerable<TEntity>> SelectAllAsync(TableMap tableMap, SelectColumn columns, Filter filter);

        Task<int> SaveAsync(TableMap tableMap, object value);

        Task<int> SaveAsync(TableMap tableMap, SelectColumn columns, object value);

        Task UpdateAsync(TableMap tableMap, Filter filter, object value);

        Task<IEnumerable<TEntity>> SelectSqlAsync(string sql, object parameters);

        Task UpdateSqlAsync(string sql, object parameters);

        Task DeleteAsync(TableMap tableMap, Filter filter);
    }
}

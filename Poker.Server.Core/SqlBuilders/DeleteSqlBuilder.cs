using Poker.Core.Maps;
using Poker.Core.Validation;

namespace Poker.Core.SqlBuilders
{
    public class DeleteSqlBuilder : SqlBuilder
    {
        public DeleteSqlBuilder(SqlDialect sqlDialect, TableMap map)
            : base(sqlDialect, map)
        {
        }

        public string Build(Filter filter, out object parameters)
        {
            Contract.RequiresNotNull(filter, "filter != null");

            var where = BuildWhere(filter, out parameters);
            var tableAlias = Dialect.TableAlias(TableMap.Table);

            return $"delete from \"{tableAlias}\" where {@where};";
        }
    }
}

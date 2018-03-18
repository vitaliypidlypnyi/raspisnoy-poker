using System.Text;
using Poker.Core.Primitives.Pagination;
using Poker.Core.Validation;
using Poker.Core.ColumnOperations;
using Poker.Core.Maps;

namespace Poker.Core.SqlBuilders
{
    public class SelectSqlBuilder : SqlBuilder
    {
        public SelectSqlBuilder(SqlDialect dialect, TableMap tableMap) : base(dialect, tableMap)
        {
        }

        public string BuildSelect(SelectColumn columns, out object parameters)
        {
            Contract.RequiresNotNull(columns, "columns != null");

            return BuildSelect(columns, new Filter(), out parameters);
        }

        public string BuildSelect(SelectColumn columns, Filter filter, out object parameters)
        {
            Contract.RequiresNotNull(columns, "columns != null");
            Contract.RequiresNotNull(filter, "filter != null");

            return BuildSelect(columns, filter, new Sorting(), out parameters);
        }

        public string BuildSelect(SelectColumn columns, Filter filter, Sorting sorting, out object parameters)
        {
            Contract.RequiresNotNull(columns, "columns != null");
            Contract.RequiresNotNull(filter, "filter != null");
            Contract.RequiresNotNull(sorting, "sorting != null");

            return BuildSelect(columns, filter, sorting, new Pagination(), out parameters);
        }

        public string BuildSelect(SelectColumn columns, Filter filter, Sorting sorting, Pagination pagination, out object parameters)
        { 
            Contract.RequiresNotNull(columns, "columns != null");
            Contract.RequiresNotNull(filter, "filter != null");
            Contract.RequiresNotNull(sorting, "sorting != null");
            Contract.RequiresNotNull(pagination, "paggination != null");

            var where = BuildWhere(filter, out parameters);
            var selectColumns = BuildColumns(columns);
            var sort = BuildSorting(sorting);
            var page = BuildPagging(pagination);
            var tableAlias = Dialect.TableAlias(TableMap.Table);

            var builder = new StringBuilder($"select {selectColumns} from {tableAlias}");
            if (!string.IsNullOrWhiteSpace(@where))
                builder.Append($" where {@where}");

            if (!string.IsNullOrWhiteSpace(sort))
                builder.Append($" order by {sort}");

            if (!string.IsNullOrWhiteSpace(page))
                builder.Append(page);

            return builder.ToString();
        }

        public string BuildSelectCount(Filter filter, out object parameters)
        {
            Contract.RequiresNotNull(filter, "filter != null");

            var where = BuildWhere(filter, out parameters);
            var tableAlias = Dialect.TableAlias(TableMap.Table);

            return $"select count(*) \"{tableAlias}\" where {@where};";
        }
    }
}

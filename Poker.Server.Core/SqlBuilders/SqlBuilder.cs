using System.Collections.Generic;
using System.Linq;
using Poker.Core.Maps;
using Poker.Core.Primitives.Pagination;
using Poker.Core.Validation;
using Poker.Core.ColumnOperations;
using Poker.Core.SqlBuilders.Infrastructure;

namespace Poker.Core.SqlBuilders
{
    public abstract class SqlBuilder
    {
        protected readonly TableMap TableMap;
        protected readonly SqlDialect Dialect;

        protected SqlBuilder(SqlDialect dialect, TableMap map)
        {
            Contract.RequiresNotNull(dialect, "dialect != null");
            Contract.RequiresNotNull(map, "map != null");

            TableMap = map;
            Dialect = dialect;
        }

        protected string BuildPagging(Pagination paggination)
        {
            return paggination.HasValue ? Dialect.PaggingSyntax(paggination.PageIndex * paggination.PageSize, paggination.PageSize) : null;
        }

        protected virtual string BuildWhere(Filter filter, out object parameters)
        {
            Contract.RequiresNotNull(filter, "filter != null");

            parameters = null;
            if (filter.Operation == null)
                return null;

            var operationVisitor = new SqlOperationVisitor(Dialect, TableMap);
            var whereText = filter.Operation.Accept(operationVisitor);

            parameters = operationVisitor.GetParameters();

            return whereText;
        }

        protected virtual string BuildColumns(SelectColumn columns)
        {
            Contract.RequiresNotNull(columns, "columns != null");

            var select = "*";
            if (columns.Columns.Any())
                select = string.Join(", ", columns.Columns.Select(Dialect.SelectColumnAlias));

            return select;
        }

        protected virtual string BuildSorting(Sorting sorting)
        {
            Contract.RequiresNotNull(sorting, "sorting != null");

            if (!sorting.SortItems.Any())
                return null;

            return string.Join(", ", sorting.SortItems.Select(item => $"{Dialect.SelectColumnAlias(item.Property)} {item.Direction.ToString()}"));
        }

        protected List<string> BuildValueProperties(object value)
        {
            var properties = value.GetType()
                                  .GetProperties()
                                  .Select(p => p.Name)
                                  .ToList();
            return properties;
        }

        public static DeleteSqlBuilder Delete(TableMap tableMap)
        {
            Contract.RequiresNotNull(tableMap, $"{nameof(tableMap)} != null");

            return new DeleteSqlBuilder(new MssqlSqlDialect(), tableMap);
        }

        public static SelectSqlBuilder Select(TableMap tableMap)
        {
            Contract.RequiresNotNull(tableMap, $"{nameof(tableMap)} != null");

            return new SelectSqlBuilder(new MssqlSqlDialect(), tableMap);
        }

        public static SaveSqlBuilder Save(TableMap tableMap)
        {
            Contract.RequiresNotNull(tableMap, $"{nameof(tableMap)} != null");

            return new SaveSqlBuilder(new MssqlSqlDialect(), tableMap);
        }

        public static UpdateSqlBuilder Update(TableMap tableMap)
        {
            Contract.RequiresNotNull(tableMap, $"{nameof(tableMap)} != null");

            return new UpdateSqlBuilder(new MssqlSqlDialect(), tableMap);
        }
    }
}

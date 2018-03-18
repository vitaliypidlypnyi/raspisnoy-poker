using Poker.Core.Validation;
using Poker.Core.Maps;

namespace Poker.Core.SqlBuilders.Infrastructure
{
    public class MssqlSqlDialect : SqlDialect
    {
        public override string SelectColumnAlias(string column)
        {
            return $"[{column}]";
        }

        public override string ColumnAlias(ColumnType column)
        {
            Contract.RequiresNotNull(column, "column != null");

            return $"[{column.Name}]";
        }

        public override string TableAlias(string table)
        {
            Contract.RequiresNotEmpty(table, $"Argument '{nameof(table)}' is not valid.");

            return $"[dbo].[{table}]";
        }

        public override string PaggingSyntax(uint offset, uint size)
        {
            return $" offset {offset} rows fetch next {size} rows only;";
        }

        public override string ParameterAlias(string parameter)
        {
            Contract.RequiresNotEmpty(parameter, $"Argument '{nameof(parameter)}' is not valid.");

            return $"@{parameter}";
        }
    }
}

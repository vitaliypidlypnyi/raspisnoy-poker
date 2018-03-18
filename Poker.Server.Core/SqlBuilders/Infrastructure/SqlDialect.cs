using Poker.Core.Maps;

namespace Poker.Core.SqlBuilders
{
    public abstract class SqlDialect
    {
        public abstract string ColumnAlias(ColumnType column);

        public abstract string SelectColumnAlias(string column);

        public abstract string TableAlias(string table);

        public abstract string PaggingSyntax(uint offset, uint size);

        public abstract string ParameterAlias(string operationName);
    }
}

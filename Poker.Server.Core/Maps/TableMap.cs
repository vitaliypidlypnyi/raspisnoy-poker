using System.Collections.Generic;
using Poker.Core.Validation;

namespace Poker.Core.Maps
{
    public abstract class TableMap
    {
        private readonly Dictionary<string, ColumnType> _columns = new Dictionary<string, ColumnType>();

        public IReadOnlyCollection<ColumnType> Columns => _columns.Values;

        public string Table { get; }

        protected TableMap(string table)
        {
            Contract.RequiresNotEmpty(table, $"Argument '{nameof(table)}' is invalid.");

            Table = table;
        }

        public void Map(ColumnType columnType)
        {
            Contract.RequiresNotNull(columnType, "columnType != null");

            _columns.Add(columnType.Name, columnType);
        }

        public void Map(string column, bool isKey = false, string dbType = null)
        {
            Contract.RequiresNotEmpty(column, $"Argument '{nameof(column)}' is invalid.");

            Map(new ColumnType(column, isKey, dbType));
        }

        public string GetDbType(string key)
        {
            return _columns[key]?.DbType;
        }

        public ColumnType Find(string column)
        {
            Contract.RequiresNotEmpty(column, $"Argument '{nameof(column)}' is invalid.");

            return _columns.TryGetValue(column, out ColumnType columnType) ? columnType : null;
        }
    }
}

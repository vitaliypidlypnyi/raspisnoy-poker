using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poker.Core.Validation;
using Poker.Core.ColumnOperations;
using Poker.Core.Maps;

namespace Poker.Core.SqlBuilders
{
    public class SaveSqlBuilder : SqlBuilder
    {
        public SaveSqlBuilder(SqlDialect dialect, TableMap map)
            : base(dialect, map)
        {
        }

        public string Build(SelectColumn columns, object value, out object parameters)
        {
            Contract.RequiresNotNull(columns, $"{nameof(columns)} != null");
            Contract.RequiresNotNull(value, $"{nameof(value)} != null");

            var tableAlias = Dialect.TableAlias(TableMap.Table);
            var valueProperties = BuildValueProperties(value);

            var insertColumns = BuildColumns(columns, valueProperties);
            var insertParameters = BuildInsertParameters(columns, valueProperties);

            parameters = value;

            var builder = new StringBuilder();
            builder.AppendLine($"insert {tableAlias} {insertColumns}");
            builder.AppendLine($"values ({insertParameters});");
            builder.AppendLine($"SELECT SCOPE_IDENTITY()");
            return builder.ToString();
        }

        private string BuildColumns(SelectColumn selectColumn, List<string> properties)
        {
            //if we don't have specific selectColumn in "selectColumn.Columns" list 
            //we need to add it from inserted properties
            var columns = selectColumn.Columns;
            if (!columns.Any())
            {
                columns = properties.ToArray();
            }

            return $"({string.Join(", ", columns.Select(Dialect.SelectColumnAlias))})";
        }

        private string BuildInsertParameters(SelectColumn selectColumn, List<string> properties)
        {
            var insertColumns = new List<string>();

            //if we don't have specific selectColumn in "selectColumn.Columns" list 
            //we need to add it from inserted properties
            var columns = selectColumn.Columns;
            if (!selectColumn.Columns.Any())
            {
                columns = properties.ToArray();
            }

            foreach (var column in columns)
            {
                var insertColumn = properties.FirstOrDefault(p => p == column);
                if (insertColumn == null)
                {
                    throw new ArgumentNullException($@"Need to add inserted value into insert query for column [{column}]");
                }
                insertColumns.Add(Dialect.ParameterAlias(insertColumn));
            }
            return string.Join(", ", insertColumns);
        }
    }
}

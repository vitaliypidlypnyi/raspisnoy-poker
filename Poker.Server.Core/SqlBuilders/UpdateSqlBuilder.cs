using System.Linq;
using System.Text;
using Dapper;
using Poker.Core.Validation;
using Poker.Core.Maps;

namespace Poker.Core.SqlBuilders
{
    public class UpdateSqlBuilder : SqlBuilder
    {
        public UpdateSqlBuilder(SqlDialect dialect, TableMap map)
            : base(dialect, map)
        {
        }

        public string Build(Filter filter, object value, out object parameters)
        {
            Contract.RequiresNotNull(filter, $"{nameof(filter)} != null");
            Contract.RequiresNotNull(value, $"{nameof(value)} != null");

            var tableAlias = Dialect.TableAlias(TableMap.Table);

            var valueProperties = BuildValueProperties(value);

            var updateColumns = string.Join(", ", valueProperties.Select(v => $"{v} = {Dialect.ParameterAlias(v)}"));
            var where = BuildWhere(filter, out parameters);

            var dynamicParameters = parameters as DynamicParameters;
            dynamicParameters?.AddDynamicParams(value);

            var builder = new StringBuilder();
            builder.AppendLine($"update {tableAlias}");
            builder.AppendLine($"set {updateColumns}");

            if (!string.IsNullOrWhiteSpace(where))
            {
                builder.AppendLine($"where {where}");
            }

            return builder.ToString();
        }
    }
}

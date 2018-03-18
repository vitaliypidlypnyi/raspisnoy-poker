using System;
using System.Linq;
using Dapper;
using Poker.Core.Maps;
using Poker.Core.SqlOperations;
using Poker.Core.Validation;

namespace Poker.Core.SqlBuilders
{
    internal class SqlOperationVisitor : IOperationVisitor
    {
        private readonly SqlDialect _dialect;

        private readonly DynamicParameters _parameters = new DynamicParameters();

        private readonly TableMap _tableMap;

        public SqlOperationVisitor(SqlDialect dialect, TableMap tableMap)
        {
            Contract.RequiresNotNull(dialect, "dialect != null");
            Contract.RequiresNotNull(tableMap, "tableMap != null");

            _dialect = dialect;
            _tableMap = tableMap;
        }

        public string Visit(PropertyOperation operation)
        {
            Contract.RequiresNotNull(operation, "operation != null");

            var column = _tableMap.Find(operation.Property);
            Contract.AssumeNotNull(column, "column != null");

            return _dialect.ColumnAlias(column);
        }

        public string Visit(EqualsOperation operation)
        {
            Contract.RequiresNotNull(operation, "operation != null");

            return $"{Visit(operation.LeftSide)} = {Visit(operation.RightSide)}";
        }

        public string Visit(TrueOperation operation)
        {
            Contract.RequiresNotNull(operation, "operation != null");

            return $"{Visit(operation.LeftSide)}";
        }

        public string Visit(NotOperation operation)
        {
            Contract.RequiresNotNull(operation, "operation != null");

            return $"not({Visit(operation.LeftSide)})";
        }

        public string Visit(AndOperation operation)
        {
            Contract.RequiresNotNull(operation, "operation != null");

            return $"{Visit(operation.LeftSide)} and {Visit(operation.RightSide)}";
        }

        public string Visit(OrOperation operation)
        {
            Contract.RequiresNotNull(operation, "operation != null");

            return $"({Visit(operation.LeftSide)} or {Visit(operation.RightSide)})";
        }

        public string Visit(ConstantOperation operation)
        {
            Contract.RequiresNotNull(operation, "operation != null");

            var parameter = _dialect.ParameterAlias(operation.Name);
            _parameters.Add(parameter, operation.Value);

            return parameter;
        }

        public string Visit(InOperation operation)
        {
            Contract.RequiresNotNull(operation, "operation != null");

            return $"{Visit(operation.LeftSide)} in ({Visit(operation.RightSide)})";
        }

        public string Visit(NullOperation operation)
        {
            Contract.RequiresNotNull(operation, "operation != null");

            return $"{Visit(operation.LeftSide)} is null";
        }

        public string Visit(ArrayOperation operation)
        {
            Contract.RequiresNotNull(operation, "operation != null");

            return string.Join(", ", operation.Items.Select(item => Visit(item)));
        }

        public string Visit(CustomOperation operation)
        {
            Contract.RequiresNotNull(operation, "operation != null");

            var parameterName = Visit(operation.Constant);
            return operation.Operation.Replace($"@{operation.Constant.Name}", parameterName);
        }

        private string Visit(Operation operation)
        {
            if (operation is PropertyOperation)
                return Visit((PropertyOperation)operation);

            if (operation is EqualsOperation)
                return Visit((EqualsOperation)operation);

            if (operation is TrueOperation)
                return Visit((TrueOperation)operation);

            if (operation is NotOperation)
                return Visit((NotOperation)operation);

            if (operation is AndOperation)
                return Visit((AndOperation)operation);

            if (operation is OrOperation)
                return Visit((OrOperation)operation);

            if (operation is ConstantOperation)
                return Visit((ConstantOperation)operation);

            if (operation is InOperation)
                return Visit((InOperation)operation);

            if (operation is NullOperation)
                return Visit((NullOperation)operation);

            if (operation is ArrayOperation)
                return Visit((ArrayOperation)operation);

            if (operation is CustomOperation)
                return Visit((CustomOperation)operation);

            throw new InvalidCastException();
        }

        public object GetParameters()
        {
            return _parameters;
        }
    }
}

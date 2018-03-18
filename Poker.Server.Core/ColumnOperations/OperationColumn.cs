using Poker.Core.Validation;

namespace Poker.Core.ColumnOperations
{
    public abstract class OperationColumn
    {
        protected OperationColumn()
            : this(new string[0])
        {

        }

        protected OperationColumn(string[] columns)
        {
            Contract.RequiresNotNull(columns, $"{nameof(columns)} != null");

            Columns = columns;
        }

        public string[] Columns { get; }
    }
}

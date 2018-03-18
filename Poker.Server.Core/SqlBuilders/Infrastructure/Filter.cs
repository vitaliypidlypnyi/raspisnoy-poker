using Poker.Core.SqlOperations;
using Poker.Core.Validation;

namespace Poker.Core.SqlBuilders
{
    public class Filter
    {
        internal Operation Operation;

        public Filter()
        {

        }

        public Filter(Operation operation)
        {
            Contract.RequiresNotNull(operation, "operation != null");

            And(operation);
        }

        public void And(Operation operation)
        {
            Contract.RequiresNotNull(operation, "operation != null");

            Operation = Operation == null ? operation : Operation.And(operation);
        }

        public void Or(Operation operation)
        {
            Contract.RequiresNotNull(operation, "operation != null");

            Operation = Operation == null ? operation : Operation.Or(operation);
        }
    }
}

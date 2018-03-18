using Poker.Core.Validation;

namespace Poker.Core.SqlOperations
{
    public abstract class UnaryOperation : Operation
    {
        public Operation LeftSide { get; private set; }

        protected UnaryOperation(Operation leftSide)
        {
            Contract.RequiresNotNull(leftSide, "leftSide != null");

            LeftSide = leftSide;
        }
    }
}

using Poker.Core.Validation;

namespace Poker.Core.SqlOperations
{
    public abstract class BinaryOperation : Operation
    {
        public Operation LeftSide { get; private set; }

        public Operation RightSide { get; private set; }

        protected BinaryOperation(Operation leftSide, Operation rightSide)
        {
            Contract.RequiresNotNull(leftSide, "leftSide != null");
            Contract.RequiresNotNull(rightSide, "rightSide != null");

            LeftSide = leftSide;
            RightSide = rightSide;
        }
    }
}

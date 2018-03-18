using System.Collections.Generic;
using Poker.Core.Validation;

namespace Poker.Core.SqlOperations
{
    public sealed class AndOperation : BinaryOperation
    {
        internal AndOperation(Operation leftSide, Operation rightSide)
            :base(leftSide, rightSide)
        {
        }

        public override string Accept(IOperationVisitor visitor)
        {
            Contract.RequiresNotNull(visitor, "visitor != null");

            return visitor.Visit(this);
        }

        public override object SatisfiedBy(Dictionary<string, object> dictionary)
        {
            Contract.RequiresNotNull(dictionary, "dictionary != null");

            return Equals(LeftSide.SatisfiedBy(dictionary), true) && Equals(RightSide.SatisfiedBy(dictionary), true);
        }
    }
}

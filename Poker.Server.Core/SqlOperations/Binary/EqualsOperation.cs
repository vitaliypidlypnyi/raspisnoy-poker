using System.Collections.Generic;
using Poker.Core.Validation;

namespace Poker.Core.SqlOperations
{
    public class EqualsOperation : BinaryOperation
    {
        internal EqualsOperation(Operation leftSide, Operation rightSide) 
            : base(leftSide, rightSide)
        {
        }

        public override string Accept(IOperationVisitor visitor)
        {
            Contract.RequiresNotNull(visitor, "visitor != null");

            return visitor.Visit(this);
        }

        public override object SatisfiedBy(Dictionary<string, object> values)
        {
            Contract.RequiresNotNull(values, "values != null");

            return Equals(LeftSide.SatisfiedBy(values), RightSide.SatisfiedBy(values));
        }
    }
}
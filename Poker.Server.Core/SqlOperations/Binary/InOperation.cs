using System.Collections.Generic;
using System.Linq;
using Poker.Core.Validation;

namespace Poker.Core.SqlOperations
{
    public class InOperation: BinaryOperation
    {
        internal InOperation(PropertyOperation leftSide, ArrayOperation operation) 
            : base(leftSide, operation)
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

            var leftValue = LeftSide.SatisfiedBy(values);
            var rightValue = RightSide.SatisfiedBy(values) as object[];

            return rightValue.Contains(leftValue);
        }
    }
}
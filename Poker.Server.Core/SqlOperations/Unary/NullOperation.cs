using System.Collections.Generic;
using Poker.Core.Validation;

namespace Poker.Core.SqlOperations
{
    public class NullOperation : UnaryOperation
    {
        internal NullOperation(Operation operation) : base(operation)
        {
        }

        public override string Accept(IOperationVisitor visitor)
        {
            Contract.RequiresNotNull(visitor, "visitor != null");

            return visitor.Visit(this);
        }

        public override object SatisfiedBy(Dictionary<string, object> dictionary)
        {
            return Equals(LeftSide.SatisfiedBy(dictionary), null);
        }
    }
}
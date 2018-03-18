using System;
using System.Collections.Generic;
using Poker.Core.Validation;

namespace Poker.Core.SqlOperations
{
    public class NotOperation : UnaryOperation
    {
        internal NotOperation(Operation operation) : base(operation)
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

            return Equals(!Convert.ToBoolean(LeftSide.SatisfiedBy(dictionary)));
        }
    }
}
using System;
using System.Collections.Generic;
using Poker.Core.Validation;

namespace Poker.Core.SqlOperations
{
    public class CustomOperation : Operation
    {
        internal CustomOperation(string operation, ConstantOperation constant)
        {
            Contract.RequiresNotEmpty(operation, $"Argument '{nameof(operation)}' is invalid.");
            Contract.RequiresNotNull(constant, "operation != null");

            Operation = operation;
            Constant = constant;
        }

        public string Operation { get; }

        public new ConstantOperation Constant { get; }

        public override string Accept(IOperationVisitor visitor)
        {
            Contract.RequiresNotNull(visitor, "visitor != null");

            return visitor.Visit(this);
        }

        public override object SatisfiedBy(Dictionary<string, object> dictionary)
        {
            throw new NotImplementedException();
        }
    }
}

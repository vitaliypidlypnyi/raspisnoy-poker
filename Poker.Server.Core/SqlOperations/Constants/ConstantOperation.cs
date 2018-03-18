using System.Collections.Generic;
using Poker.Core.Validation;

namespace Poker.Core.SqlOperations
{
    public class ConstantOperation : ValueOperation
    {
        internal ConstantOperation(string name, object value)
        {
            Contract.RequiresNotEmpty(name, $"Argument '{nameof(name)}' is invalid.");

            Name = name;
            Value = value;
        }

        public string Name { get; }

        public object Value { get; }

        public override string Accept(IOperationVisitor visitor)
        {
            Contract.RequiresNotNull(visitor, "visitor != null");

            return visitor.Visit(this);
        }

        public override object SatisfiedBy(Dictionary<string, object> dictionary)
        {
            return Value;
        }
    }
}
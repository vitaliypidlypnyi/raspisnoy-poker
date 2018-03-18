using System.Collections.Generic;
using Poker.Core.Validation;

namespace Poker.Core.SqlOperations
{
    public class PropertyOperation : Operation
    {
        internal PropertyOperation(string property)
        {
            Contract.RequiresNotEmpty(property, $"Argument '{nameof(property)}' is invalid.");

            Property = property;
        }

        public string Property { get; }

        public override string Accept(IOperationVisitor visitor)
        {
            Contract.RequiresNotNull(visitor, "visitor != null");

            return visitor.Visit(this);
        }

        public override object SatisfiedBy(Dictionary<string, object> dictionary)
        {
            Contract.RequiresNotNull(dictionary, "dictionary != null");

            return dictionary.TryGetValue(Property, out object value) ? value : null;
        }
    }
}
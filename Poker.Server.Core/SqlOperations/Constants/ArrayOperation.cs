using System.Collections.Generic;
using System.Linq;
using Poker.Core.Validation;

namespace Poker.Core.SqlOperations
{
    public class ArrayOperation : ValueOperation
    {
        internal ArrayOperation(string name, params object[] items)
        {
            Contract.RequiresNotNull(items, "items != null");

            Items = items.Select((item, index) => new ConstantOperation($"{name}{index}", item)).ToArray();
        }

        public ConstantOperation[] Items { get; }


        public override string Accept(IOperationVisitor visitor)
        {
            Contract.RequiresNotNull(visitor, "visitor != null");

            return visitor.Visit(this);
        }

        public override object SatisfiedBy(Dictionary<string, object> dictionary)
        {
            return Items.Select(item => item.Value).ToArray();
        }
    }
}
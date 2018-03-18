using Poker.Core.Validation;

namespace Poker.Core.Primitives.Pagination
{
    public class SortItem
    {
        public SortItem(string property, SortDirection direction)
        {
            Contract.RequiresNotNull(property, "sortProperty != null");
            Contract.RequiresEnum<SortDirection>(direction, $"Argument {nameof(direction)} is invalid.");

            Property = property;
            Direction = direction;
        }

        public string Property { get; }

        public SortDirection Direction { get; }
    }
}
